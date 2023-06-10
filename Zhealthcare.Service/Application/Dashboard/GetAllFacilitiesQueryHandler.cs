using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Dashboard
{
    public class GetAllFacilitiesQueryHandler : IRequestHandler<GetAllFacilitiesQuery, IEnumerable<string>>
    {
        private readonly IRepository<Patient> _repository;

        public GetAllFacilitiesQueryHandler(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetAllFacilitiesQuery request, CancellationToken cancellationToken)
        {
            var query = new QueryDefinition("select distinct c.facilityId from c");
            var facilities = await _repository.GetByQueryAsync(query, cancellationToken);
            return facilities.Where(x=>!string.IsNullOrEmpty(x.FacilityId))
                .Select(x => x.FacilityId);
        }
    }
}
