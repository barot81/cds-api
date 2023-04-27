using MediatR;
using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Commands
{
    public class DeletePatientByIdCommandHandler : IRequestHandler<DeletePatientByIdCommand, bool>
    {
        private readonly IRepository<Patient> _repository;

        public DeletePatientByIdCommandHandler(IRepository<Patient> repository)
        => _repository = repository;

        public async Task<bool> Handle(DeletePatientByIdCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var patient = await _repository.GetAsync(command.Id.ToString(),command.FacilityId, cancellationToken);
                await _repository.DeleteAsync(patient, cancellationToken);
                return true;
            } 
            catch(Exception)
            {
                return false;
            }
            
        }
    }
}
