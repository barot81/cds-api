using MediatR;
using Zhealthcare.Service.Domain.Entities.Lookup;

namespace Zhealthcare.Service.Application.Lookups
{
    public record AddLookupCommand<T>(Lookup<T> Lookup) : IRequest<bool> where T:ILookupItem;
}
