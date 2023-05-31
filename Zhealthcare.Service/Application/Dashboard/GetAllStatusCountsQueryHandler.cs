using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Dashboard
{
    public class GetAllStatusCountsQueryHandler : IRequestHandler<GetAllStatusCountsQuery, IEnumerable<StatusCount>>
    {

        private readonly IRepository<Patient> _repository;

        public GetAllStatusCountsQueryHandler(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StatusCount>> Handle(GetAllStatusCountsQuery request, CancellationToken cancellationToken)
        {
            var query = new QueryDefinition("select c.reviewStatus from c where c.facilityId = @facilityId")
                            .WithParameter("@facilityId", request.FacilityId);
            var reviewStatuses = await _repository.GetByQueryAsync(query, cancellationToken);

            return reviewStatuses
                           .GroupBy(x => x.ReviewStatus)
                           .Select(x => new StatusCount(x.Key, x.Count()));
        }
    }
}
