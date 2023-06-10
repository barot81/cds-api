using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{

    public class UpdatePatientCommentCommandHandler : IRequestHandler<UpdatePatientCommentRequest, Guid>
    {
        private readonly IRepository<Patient> _repository;

        public UpdatePatientCommentCommandHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<Guid> Handle(UpdatePatientCommentRequest request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetAsync(request.Id.ToString(), request.FacilityId, cancellationToken);
            patient.ReviewStatus = request.ReviewStatus;
            patient.GeneralComment = request.GeneralComments;
            var result = await _repository.UpdateAsync(patient, false, cancellationToken);
            return result == null ? Guid.Empty : Guid.Parse(result.Id);
        }
    }
}
