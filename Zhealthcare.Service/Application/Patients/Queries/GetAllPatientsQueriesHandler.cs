using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public class GetAllPatientsQueriesHandler : IRequestHandler<GetAllPatientsQueriesRequest, PageResponseModel>
    {
        private readonly IRepository<Patient> _patientRepository;

        public GetAllPatientsQueriesHandler(IRepository<Patient> patientRepository)
        => _patientRepository = patientRepository;

        public async Task<PageResponseModel> Handle(GetAllPatientsQueriesRequest request, CancellationToken cancellationToken)
        {
            QueryDefinition countQuery = request.GetQueryDefination("c.id");
            var countResult = await _patientRepository.GetByQueryAsync(countQuery, cancellationToken);

            QueryDefinition getAllQuery = request.GetQueryDefination("*", true, true);
            
            var data = await _patientRepository.GetByQueryAsync(getAllQuery, cancellationToken);
            return new PageResponseModel
            {
                Result = data.AsEnumerable(),
                Count = countResult.Count(),
                CurrentPage = (request.FilterModel.Start / request.FilterModel.PageSize) + 1
            };
        }

        

        
        public Func<Patient, bool> GetFilters(PatientFilter? filters)
        {
            List<Func<Patient, bool>> patientPredicate = new();
            if (filters != null)
            {
                var statusFilter = filters?.Status;
                if (statusFilter != null && statusFilter.Any())
                    patientPredicate.Add(x => statusFilter.Contains(x.ReviewStatus));

                var queryStatusFilter = filters?.QueryStatus;
                if (queryStatusFilter != null && queryStatusFilter.Any())
                    patientPredicate.Add(x => queryStatusFilter.Contains(x.QueryStatus));

                DateTime? admStartDate = filters?.AdmissionStartDate;
                DateTime? admEndDate = filters?.AdmissionEndDate;
                if (admStartDate != null && admEndDate != null)
                    patientPredicate.Add(x => x.AdmitDate >= admStartDate && x.AdmitDate <= admEndDate);

                DateTime? disStartDate = filters?.DischargeStartDate;
                DateTime? disEndDate = filters?.DischargeEndDate;
                if (disStartDate != null && disEndDate != null)
                    patientPredicate.Add(x => x.DischargeDate >= disStartDate && x.DischargeDate <= disEndDate);

                return And(patientPredicate.ToArray());
            }
            return x => true;


        }
        public static Func<Patient, bool> And(params Func<Patient, bool>[] predicates)
        {
            return delegate (Patient item)
            {
                foreach (Func<Patient, bool> predicate in predicates)
                {
                    if (!predicate(item))
                    {
                        return false;
                    }
                }
                return true;
            };
        }
    }
}
