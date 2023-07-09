using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Zhealthcare.Service.Application.PatientFindings.Models;
using Zhealthcare.Service.Application.Patients.Commands;
using Zhealthcare.Service.Application.Patients.Queries;

namespace Zhealthcare.Service.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    [RequiredScope("patients.read")]
    public class PatientsFindingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientsFindingController(IMediator mediator)
        => _mediator = mediator;
        

        [HttpPost("{FacilityId}/patients/{PatientId}/findings")]
        public async Task<IActionResult> Create(string FacilityId,Guid PatientId,[FromBody] PatientFindingDto PatientFindingInfo)
        => Ok(await _mediator.Send(new CreatePatientFindingCommand(FacilityId, PatientId, PatientFindingInfo)));
        

        [HttpGet("{FacilityId}/patients/{PatientId}/findings")]
        public async Task<IActionResult> GetAll(string FacilityId, Guid PatientId)
        => Ok(await _mediator.Send(new GetAllPatientFindingsQuery(FacilityId, PatientId)));
        

        [HttpPut("{FacilityId}/patients/{PatientId}/findings/{findingId}")]
        public async Task<IActionResult> Update(string FacilityId, Guid PatientId, Guid Id, PatientFindingUpdateDto updatedPatientFinding)
        => Ok(await _mediator.Send(new UpdatePatientFindingsCommand(FacilityId, PatientId, Id, updatedPatientFinding)));
        

        [HttpDelete("{FacilityId}/patients/{PatientId}/findings/{findingId}")]
        public async Task<IActionResult> Delete(string FacilityId, Guid PatientId, Guid Id, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new DeletePatientFindingsByIdCommand(FacilityId, PatientId, Id), cancellationToken));
    }
}