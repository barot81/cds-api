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
            var query = new QueryDefinition("select c.reviewStatus from c where c.facilityId = @facilityId AND c.entityName = @entityName")
                            .WithParameter("@facilityId", request.FacilityId)
                            .WithParameter("@entityName", nameof(Patient));
            var reviewStatuses = await _repository.GetByQueryAsync(query, cancellationToken);

            var totalCountQuery = new QueryDefinition("select c.id from c where c.entityName = @entityName")
                            .WithParameter("@entityName", nameof(Patient));
            var totalIds = await _repository.GetByQueryAsync(totalCountQuery, cancellationToken);
            
            var statistics = new List<StatusCount>() { new StatusCount("Total", totalIds.Count()) };
            return statistics.Concat(
                        reviewStatuses
                           .GroupBy(x => x.ReviewStatus)
                           .Select(x => new StatusCount(x.Key, x.Count())));
        }
    }
}
