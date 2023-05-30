using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Dashboard
{
    public class GetAllFacilitiesQueryHandler : IRequestHandler<GetAllFacilitiesQuery, IEnumerable<FacilitiesResponse>>
    {
        private readonly IRepository<Patient> _repository;

        public GetAllFacilitiesQueryHandler(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FacilitiesResponse>> Handle(GetAllFacilitiesQuery request, CancellationToken cancellationToken)
        {
            var query = new QueryDefinition("select c.facilityId, c.attendingPhysician from c");
            var facilities = await _repository.GetByQueryAsync(query, cancellationToken);
            return facilities
                .GroupBy(x => x.FacilityId)
                .Select(x => new FacilitiesResponse
                {
                    FacilityId = x.Key,
                    Physicians = x.Select(y => y.AttendingPhysician).Distinct().ToList()
                });
        }
    }
}
