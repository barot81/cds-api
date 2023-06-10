using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{

    public class UpdatePatientReviewStatusCommandHandler : IRequestHandler<UpdatePatientReviewStatusRequest, Guid>
    {
        private readonly IRepository<Patient> _repository;

        public UpdatePatientReviewStatusCommandHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Guid> Handle(UpdatePatientReviewStatusRequest request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetAsync(request.Id.ToString(), request.FacilityId, cancellationToken);
            patient.ReviewStatus = request.ReviewStatus;
            var result = await _repository.UpdateAsync(patient, false, cancellationToken);
            return result == null ? Guid.Empty : Guid.Parse(result.Id);
        }
    }
}
