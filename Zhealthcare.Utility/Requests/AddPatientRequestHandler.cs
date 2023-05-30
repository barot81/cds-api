using MediatR;
using Zhealthcare.Service.Application.Patients.Commands;
using Zhealthcare.Utility.Services;

namespace Zhealthcare.Utility.Requests
{
    internal class AddPatientRequestHandler : IRequestHandler<AddPatientRequest, bool>
    {
        private readonly IMediator _mediator;
        public AddPatientRequestHandler(IMediator mediator)
        => _mediator = mediator;

        public async Task<bool> Handle(AddPatientRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var createPatientCommand = new CreatePatientCommand(request.PatientDto.FacilityId, request.PatientDto);
                var result = await _mediator.Send(createPatientCommand);
                return result != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
