using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities.Drg;
using Zhealthcare.Service.Domain.Entities.Lookup;

namespace Zhealthcare.Service.Application.Lookups
{
    
    public class GetLookupbyIdRequestHandler 
        : IRequestHandler<GetMsDrgLookupRequest, Lookup<MsDrgLookupItem>>,
         IRequestHandler<GetAprDrgLookupRequest, Lookup<AprDrgLookupItem>>,
         IRequestHandler<GetDiagnosisLookupRequest, Lookup<DiagnosisLookupItem>>,
         IRequestHandler<GetReimbursementTypeLookupRequest, Lookup<ReimbursementTypeLookupItem>>
    {

        private readonly IRepository<Lookup<MsDrgLookupItem>> _msDrgLookupRepository;
        private readonly IRepository<Lookup<AprDrgLookupItem>> _aprDrgLookupRepository;
        private readonly IRepository<Lookup<DiagnosisLookupItem>> _diagnosisLookupRepository;
        private readonly IRepository<Lookup<ReimbursementTypeLookupItem>> _reimbursementTypeRepository;

        public GetLookupbyIdRequestHandler(
            IRepository<Lookup<MsDrgLookupItem>> msDrgLookupRepository,
            IRepository<Lookup<AprDrgLookupItem>> aprDrgLookupRepository,
            IRepository<Lookup<DiagnosisLookupItem>> diagnosisLookupRepository,
            IRepository<Lookup<ReimbursementTypeLookupItem>> reimbursementTypeRepository
            )
        {
            _msDrgLookupRepository = msDrgLookupRepository;
            _aprDrgLookupRepository =  aprDrgLookupRepository;
            _diagnosisLookupRepository= diagnosisLookupRepository;
            _reimbursementTypeRepository = reimbursementTypeRepository;
  
        }

        public async Task<Lookup<ReimbursementTypeLookupItem>> Handle(GetReimbursementTypeLookupRequest request, CancellationToken cancellationToken)
        => await _reimbursementTypeRepository.GetAsync("Lookups.ReimbursementType", nameof(Lookup<ReimbursementTypeLookupItem>), cancellationToken);

        public async Task<Lookup<DiagnosisLookupItem>> Handle(GetDiagnosisLookupRequest request, CancellationToken cancellationToken)
        => await _diagnosisLookupRepository.GetAsync("Lookups.QueryDiagnosis", nameof(Lookup<DiagnosisLookupItem>), cancellationToken);

        public async Task<Lookup<AprDrgLookupItem>> Handle(GetAprDrgLookupRequest request, CancellationToken cancellationToken)
        => await _aprDrgLookupRepository.GetAsync("Lookups.AprDrg", nameof(Lookup<AprDrgLookupItem>), cancellationToken);

        public async Task<Lookup<MsDrgLookupItem>> Handle(GetMsDrgLookupRequest request, CancellationToken cancellationToken)
        => await _msDrgLookupRepository.GetAsync("Lookups.MsDrg", nameof(Lookup<MsDrgLookupItem>), cancellationToken);
        
    }
}
