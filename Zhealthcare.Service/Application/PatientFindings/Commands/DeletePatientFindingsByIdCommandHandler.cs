using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public class DeletePatientFindingsByIdCommandHandler : IRequestHandler<DeletePatientFindingsByIdCommand, bool>
    {
        private readonly IRepository<Patient> _repository;

        public DeletePatientFindingsByIdCommandHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<bool> Handle(DeletePatientFindingsByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var patientFinding = await _repository.GetAsync(command.FindingId.ToString(), command.FacilityId, cancellationToken);
                await _repository.DeleteAsync(patientFinding, cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
