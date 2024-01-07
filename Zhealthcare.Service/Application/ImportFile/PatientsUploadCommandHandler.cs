using Mapster;
using MediatR;
using Zhealthcare.Service.Application.Patients.Commands;
using Zhealthcare.Service.Application.Patients.Models;
using Zhealthcare.Service.Application.Patients.Queries;
using Zhealthcare.Service.Helper;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.ImportFile
{
    public class PatientsUploadCommandHandler : IRequestHandler<PatientsUploadCommand, ImportFileResponse>
    {
        private readonly IMediator _mediator;
        private readonly UserContext _userContext;
        public PatientsUploadCommandHandler(IMediator mediator, UserContext userContext)
        {
            _mediator = mediator;
            _userContext = userContext;
        }

        public async Task<ImportFileResponse> Handle(PatientsUploadCommand request, CancellationToken cancellationToken)
        {
            var patients = FileReader.LoadCsvData<PatientDto>("Data", request.FileName);
            var patientNos = patients.Select(x => x.PatientNo).ToList();
            var existingPatients = await _mediator.Send(new GetAllNonDischagePatientsQuery(request.FacilityId), cancellationToken);
            HashSet<long> existingPatientNos = new(existingPatients.Select(x => x.PatientNo));
            var newPatients = patients.Where(x=> !existingPatientNos.Contains(x.PatientNo));
            foreach(var patient in newPatients)
            {
                await _mediator.Send(new CreatePatientCommand(request.FacilityId, patient), cancellationToken);
            }
            foreach(var patient in existingPatients)
            {
                if (!patientNos.Contains(patient.PatientNo))
                    patient.DischargeDate = DateTime.UtcNow.AddDays(-1);
                await _mediator.Send(new UpdatePatientCommand(patient.Id,request.FacilityId, patient.Adapt<PatientUpdateDto>(), _userContext.Name), cancellationToken);
            }
            return new ImportFileResponse(true, Enumerable.Empty<ErrorDetail>());
        }
    }
}
