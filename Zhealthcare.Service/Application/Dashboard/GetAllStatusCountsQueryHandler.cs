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
            var query = new QueryDefinition("select c.reviewStatus, c.dischageDate from c where c.facilityId = @facilityId AND c.entityName = @entityName")
                            .WithParameter("@facilityId", request.FacilityId)
                            .WithParameter("@entityName", nameof(Patient));
            var reviewStatuses = (await _repository.GetByQueryAsync(query, cancellationToken)) ?? new List<Patient>();
            var censusRecords = reviewStatuses.Where(x => x.DischargeDate == null);
            var statistics = new List<StatusCount>() { 
                new StatusCount("Total", censusRecords.Count(x=> x.ReviewStatus != "Non DRG")),
                new StatusCount("Pending Query", reviewStatuses.Count(x=>x.ReviewStatus == "Pending Query"))
            };

            var count = statistics.Concat(
                        censusRecords
                           .Where(x=> x.ReviewStatus != "Pending Query")
                           .GroupBy(x => x.ReviewStatus)
                           .Select(x => new StatusCount(x.Key, x.Count())));
            return count;
        }
    }
}
