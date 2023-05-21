using Exxat.Meta.Infrastructure.CosmosDb;
using MapsterMapper;
using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public class GetAllPatientsQueriesHandler : IRequestHandler<GetAllPatientsQueriesRequest, PageResponseModel>
    {
        private readonly IRepository<Patient> _patientRepository;

        public GetAllPatientsQueriesHandler(IRepository<Patient> patientRepository)
        => _patientRepository = patientRepository;
        
        public async Task<PageResponseModel> Handle(GetAllPatientsQueriesRequest request, CancellationToken cancellationToken)
        {
            var count = await _patientRepository.CountAsync(x=> x.Type == nameof(Patient) && x.PartitionKey == request.FacilityId, cancellationToken);
            var data = await _patientRepository.QueryAsync(new PatientFilterSpecification(request.FacilityId, request.FilterModel), cancellationToken);
            return new PageResponseModel
            {
                Result = data.Items.AsEnumerable(),
                Count = count,
                CurrentPage = (request.FilterModel.Start / request.FilterModel.PageSize) + 1
            };
        }
    }
}
