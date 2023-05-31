using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Dashboard
{
    public class GetPhysiciansQueryHandler : IRequestHandler<GetPhysiciansQuery, IEnumerable<string>>
    {

        private readonly IRepository<Patient> _repository;

        public GetPhysiciansQueryHandler(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> Handle(GetPhysiciansQuery request, CancellationToken cancellationToken)
        {
            var query = new QueryDefinition("select distinct c.attendingPhysician from c where c.attendingPhysician!='' And c.facilityId = @facilityId")
                            .WithParameter("@facilityId", request.FacilityId);
            var physicians = await _repository.GetByQueryAsync(query, cancellationToken);

            return physicians.Select(x => x.AttendingPhysician);
        }
    }
}
