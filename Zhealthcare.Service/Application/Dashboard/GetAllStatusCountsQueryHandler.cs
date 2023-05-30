using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Dashboard
{
    public class GetAllStatusCountsQueryHandler : IRequestHandler<GetAllStatusCountsQuery, StatusCountResponse>
    {

        private readonly IRepository<Patient> _repository;

        public GetAllStatusCountsQueryHandler(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        public async Task<StatusCountResponse> Handle(GetAllStatusCountsQuery request, CancellationToken cancellationToken)
        {
            var query = new QueryDefinition("select c.reviewStatus from c where c.facilityId = @facilityId")
                            .WithParameter("@facilityId", request.FacilityId);
            var reviewStatuses = await _repository.GetByQueryAsync(query, cancellationToken);

            return new StatusCountResponse(reviewStatuses
                           .GroupBy(x => x.ReviewStatus)
                           .ToDictionary(x => x.Key, y => y.Count())
                        );
        }
    }
}
