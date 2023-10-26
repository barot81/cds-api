using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Domain.Entities.Drg;
using Zhealthcare.Service.Domain.Entities.Lookup;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public class GetAllPatientsQueriesHandler : IRequestHandler<GetAllPatientsQueriesRequest, PageResponseModel>
    {
        private readonly IRepository<Patient> _patientRepository;
        private readonly IRepository<Lookup<MsDrgLookupItem>> _msDrgLookupRepository;
        private readonly IRepository<Lookup<AprDrgLookupItem>> _aprDrgLookupRepository;

        public GetAllPatientsQueriesHandler(IRepository<Patient> patientRepository,
             IRepository<Lookup<MsDrgLookupItem>> msDrgLookupRepository,
            IRepository<Lookup<AprDrgLookupItem>> aprDrgLookupRepository)
        {
            _patientRepository = patientRepository;
            _aprDrgLookupRepository = aprDrgLookupRepository;
            _msDrgLookupRepository = msDrgLookupRepository;
        }

        public async Task<PageResponseModel> Handle(GetAllPatientsQueriesRequest request, CancellationToken cancellationToken)
        {
            QueryDefinition countQuery = request.GetQueryDefination("c.id");
            var countResult = await _patientRepository.GetByQueryAsync(countQuery, cancellationToken);

            QueryDefinition getAllQuery = request.GetQueryDefination("*", true, true);

            var data = await _patientRepository.GetByQueryAsync(getAllQuery, cancellationToken);
            data = await SetDrgInformation(data, cancellationToken);
            return new PageResponseModel
            {
                Result = data,
                Count = countResult.Count(),
                CurrentPage = (request.FilterModel.Start / request.FilterModel.PageSize) + 1
            };
        }

        private async Task<IEnumerable<Patient>> SetDrgInformation(IEnumerable<Patient> data, CancellationToken cancellationToken)
        {
            var msDrgNos = data.Where(x => x.ReimbursementType == "MS-DRG").Select(x => x.DrgNo).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var aprDrgNos = data.Where(x => x.ReimbursementType == "APR-DRG").Select(x => x.DrgNo).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var msDrgLookups = (await _msDrgLookupRepository.GetAsync("Lookups.MsDrg", nameof(Lookup<MsDrgLookupItem>), cancellationToken))
                    .Items.Where(x=> msDrgNos.Contains(x.DrgNo));
            var aprDrgLookups = (await _aprDrgLookupRepository.GetAsync("Lookups.AprDrg", nameof(Lookup<AprDrgLookupItem>), cancellationToken))
                    .Items.Where(x => aprDrgNos.Contains(x.DrgNo));
            foreach (var item in data.ToList())
            {
                if (msDrgNos.Contains(item.DrgNo)) {
                    var currentDrg = msDrgLookups.FirstOrDefault(x => x.DrgNo == item.DrgNo);
                    item.DrgWeight = currentDrg.Weights;
                    item.DrgType = currentDrg.DrgType;
                    item.TransferDrg = currentDrg.IsPostAcuteDrg;
                }
                else if (aprDrgNos.Contains(item.DrgNo))
                {
                    var currentDrg = aprDrgLookups.FirstOrDefault(x => x.DrgNo == item.DrgNo);
                    item.DrgWeight = currentDrg.RelativeWeight;
                    item.DrgType = currentDrg.PediatricMedicaidCareCategory;
                    item.TransferDrg = "-";
                }
                
            }
            return data;
            //var q = new QueryDefinition("SELECT value(item) FROM c JOIN item IN c.items WHERE c.id = @id AND  ARRAY_CONTAINS(@msDrgNos, item.drgNo, true)")
            //    .WithParameter("@id", "Lookups.MsDrg")
            //    .WithParameter("@msDrgNos", msDrgNos);
            //var msDrgLookups = await _msDrgLookupRepository.GetByQueryAsync(
            //    q.QueryText,
            //    cancellationToken);
            //var aprDrgLookups = await _aprDrgLookupRepository.GetByQueryAsync(
            //    $"SELECT value(item) FROM c JOIN item IN c.items WHERE c.id = 'Lookups.AprDrg' AND ARRAY_CONTAINS({aprDrgNos}, item.drgNo, true)",
            //    cancellationToken);
        }

        public Func<Patient, bool> GetFilters(PatientFilter? filters)
        {
            List<Func<Patient, bool>> patientPredicate = new();
            if (filters != null)
            {
                var statusFilter = filters?.ReviewStatus;
                if (statusFilter != null && statusFilter.Any())
                    patientPredicate.Add(x => statusFilter.Contains(x.ReviewStatus));

                var queryStatusFilter = filters?.QueryStatus;
                if (queryStatusFilter != null && queryStatusFilter.Any())
                    patientPredicate.Add(x => queryStatusFilter.Contains(x.QueryStatus));

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
