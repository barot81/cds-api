using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Lookups
{
    public class AddLookupCommandHandler : IRequestHandler<AddLookupCommand, bool>
    {
        private readonly IRepository<Lookup> _repository;
        public AddLookupCommandHandler(IRepository<Lookup> repository)
        {
            _repository= repository;
        }
        public async Task<bool> Handle(AddLookupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lookup = request.Lookup;
                lookup.Type = nameof(Lookup);
                lookup.PartitionKey = nameof(Lookup);
                await _repository.CreateAsync(request.Lookup, cancellationToken);
                return true;
            }
            catch(Exception ex) {
                return false;
            }
        }
    }
}
