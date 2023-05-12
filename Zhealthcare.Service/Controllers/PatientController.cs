using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zhealthcare.Service.Application.Patients.Commands;
using Zhealthcare.Service.Application.Patients.Models;
using Zhealthcare.Service.Application.Patients.Queries;

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
        [HttpPost("{FacilityId}/patients")]
        public async Task<IActionResult> Create(string FacilityId, PatientDto PatientInfo)
        {
            return Ok(await _mediator.Send(new CreatePatientCommand(FacilityId, PatientInfo)));
        }

        [HttpGet("{FacilityId}/patients")]
        public async Task<IActionResult> GetAll(string FacilityId)
        {
            return Ok(await _mediator.Send(new GetAllPatientsQuery(FacilityId)));
        }

        [HttpGet("{FacilityId}/patients/{id}")]
        public async Task<IActionResult> GetById(string FacilityId, Guid id)
        {
            return Ok(await _mediator.Send(new GetPatientsByIdQuery(FacilityId,id)));
        }

        [HttpPut("{FacilityId}/patients/{id}")]
        public async Task<IActionResult> Update(string FacilityId, Guid Id, [FromBody] PatientUpdateDto updatedPatient)
        {
            return Ok(await _mediator.Send(new UpdatePatientCommand(Id.ToString(), FacilityId, updatedPatient)));
        }

        [HttpDelete("{FacilityId}/patients/{id}")]
        public async Task<IActionResult> Delete(string FacilityId, Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new DeletePatientByIdCommand(FacilityId, id), cancellationToken));
        }
    }
}