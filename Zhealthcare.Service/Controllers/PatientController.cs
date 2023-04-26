using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zhealthcare.Service.Application.Commands;
using Zhealthcare.Service.Application.Models;
using Zhealthcare.Service.Application.Queries;

namespace Zhealthcare.Service.Controllers
{
    [Route("api/")]
    [ApiController]

    public class PatientsController : ControllerBase
    {


        private readonly IMediator _mediator;
        public PatientsController(IMediator mediator)
        => _mediator = mediator;
        

        // URL - https://localhost:44378/api/Patient type POST
        [HttpPost("Facilities/{FacilityId}/patients")]
        public async Task<IActionResult> Create(string FacilityId, PatientDto PatientInfo)
        {
            return Ok(await _mediator.Send(new CreatePatientCommand(FacilityId, PatientInfo)));
        }

        [HttpGet("Facilities/{FacilityId}/patients")]
        public async Task<IActionResult> GetAll(string FacilityId)
        {
            return Ok(await _mediator.Send(new GetAllPatientQuery(FacilityId)));
        }

        [HttpGet("Facilities/{FacilityId}/patients/{id}")]
        public async Task<IActionResult> GetById(string FacilityId, Guid id)
        {
            return Ok(await _mediator.Send(new GetPatientByIdQuery(FacilityId,id)));
        }

        [HttpPut("Facilities/{FacilityId}/patients/{id}")]
        public async Task<IActionResult> Update(Guid Id, PatientUpdateDto updatedPatient, string FacilityId)
        {
            return Ok(await _mediator.Send(new UpdatePatientCommand(Id.ToString(), FacilityId, updatedPatient)));
        }

        [HttpDelete("Facilities/{FacilityId}/patients/{id}")]
        public async Task<IActionResult> Delete(string FacilityId, Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new DeletePatientByIdCommand(id,FacilityId), cancellationToken));
        }
    }
}