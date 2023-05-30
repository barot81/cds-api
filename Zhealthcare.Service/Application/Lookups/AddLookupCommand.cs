using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Lookups
{
    public record AddLookupCommand(Lookup Lookup) : IRequest<bool>;
}
