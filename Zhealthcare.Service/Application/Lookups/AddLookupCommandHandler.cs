using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities.Lookup;

namespace Zhealthcare.Service.Application.Lookups
{
    public class AddLookupCommandHandler : IRequestHandler<AddLookupCommand<ILookupItem>, bool>
    {
        private readonly IRepository<Lookup<ILookupItem>> _repository;
        public AddLookupCommandHandler(IRepository<Lookup<ILookupItem>> repository)
        {
            _repository= repository;
        }
        public async Task<bool> Handle(AddLookupCommand<ILookupItem> request, CancellationToken cancellationToken)
        {
            try
            {
                var lookup = request.Lookup;
                lookup.Type = nameof(Lookup<ILookupItem>);
                lookup.PartitionKey = nameof(Lookup<ILookupItem>);
                await _repository.CreateAsync(request.Lookup, cancellationToken);
                return true;
            }
            catch(Exception ex) {
                return false;
            }
        }
    }
}
