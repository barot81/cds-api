using MediatR;
using Zhealthcare.Service.Domain.Entities.Drg;
using Zhealthcare.Service.Domain.Entities.Lookup;

namespace Zhealthcare.Service.Application.Lookups
{

    public record GetMsDrgLookupRequest() : IRequest<Lookup<MsDrgLookupItem>>;
    public record GetAprDrgLookupRequest() : IRequest<Lookup<AprDrgLookupItem>>;
    public record GetDiagnosisLookupRequest() : IRequest<Lookup<DiagnosisLookupItem>>;
    public record GetReimbursementTypeLookupRequest() : IRequest<Lookup<ReimbursementTypeLookupItem>>;

        

        

        
}
