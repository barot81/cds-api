using Exxat.Meta.Infrastructure.CosmosDb;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
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
            var filterQuery = GetQueryString(request.FilterModel.Filters);
            var parameterizedQuery = new QueryDefinition(
                "SELECT c.id FROM c WHERE c.partitionKey = @partitionKey and c.entityName = @type"
                + filterQuery
                )
                .WithParameter("@partitionKey", request.FacilityId)
                .WithParameter("@type", nameof(Patient))
                .WithParameter("@statuses", string.Join("','",request.FilterModel?.Filters?.Status ?? Array.Empty<string>()))
                .WithParameter("@queryStatuses", string.Join("','", request.FilterModel?.Filters?.QueryStatus ?? Array.Empty<string>()))
                .WithParameter("@admissionStartDate", request.FilterModel?.Filters?.AdmissionStartDate)
                .WithParameter("@admissionEndDate", request.FilterModel?.Filters?.AdmissionEndDate)
                .WithParameter("@dischargeStartDate", request.FilterModel?.Filters?.DischargeStartDate)
                .WithParameter("@dischargeEndDate", request.FilterModel?.Filters?.DischargeEndDate);

                 var countResult = await _patientRepository.GetByQueryAsync(parameterizedQuery, cancellationToken);
          
            var data = await _patientRepository.QueryAsync(new PatientFilterSpecification(request.FacilityId, request.FilterModel), cancellationToken);
            return new PageResponseModel
            {
                Result = data.Items.AsEnumerable(),
                Count = countResult.Count(),
                CurrentPage = (request.FilterModel.Start / request.FilterModel.PageSize) + 1
            };
        }

        private string GetQueryString(PatientFilter? filters)
        {
            var filterQuery = "";
            if (filters == null)
                return "";
            if (filters.Status != null)
                filterQuery = " AND ARRAY_CONTAINS(['@statuses'], c.status)";
            if (filters.QueryStatus != null)
                filterQuery = " AND ARRAY_CONTAINS(['@queryStatuses'], c.queryStatus)";
            if (filters.AdmissionStartDate != null && filters.AdmissionEndDate != null)
                filterQuery = " AND c.admissionDate >= @admissionStartDate &&  c.admissionDate <=  @admissionEndDate";
            if (filters.DischargeStartDate != null && filters.DischargeEndDate != null)
                filterQuery = " AND c.dischargeDate >= @dischargeStartDate &&  c.dischargeDate <=  @dischargeEndDate";

            return filterQuery;
        }

        public Func<Patient, bool> GetFilters(PatientFilter? filters)
        {
            List<Func<Patient, bool>> patientPredicate = new();
            if (filters != null)
            {
                var statusFilter = filters?.Status;
                if (statusFilter != null && statusFilter.Any())
                    patientPredicate.Add(x => statusFilter.Contains(x.Status));

                var queryStatusFilter = filters?.QueryStatus;
                if (queryStatusFilter != null && queryStatusFilter.Any())
                    patientPredicate.Add(x => queryStatusFilter.Contains(x.QueryStatus));

                DateTime? admStartDate = filters?.AdmissionStartDate;
                DateTime? admEndDate = filters?.AdmissionEndDate;
                if (admStartDate != null && admEndDate != null)
                    patientPredicate.Add(x => x.AdmissionDate >= admStartDate && x.AdmissionDate <= admEndDate);

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
