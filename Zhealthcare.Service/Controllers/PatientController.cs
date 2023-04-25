using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zhealthcare.Service.Application.Commands;
using Zhealthcare.Service.Application.Queries;

namespace Zhealthcare.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PatientController : ControllerBase
    {


        private IMediator _mediator;
        private string FacilityId;
        public PatientController(IMediator mediator)
        {
            this._mediator = mediator;
            FacilityId = "";
        }

        // URL - https://localhost:44378/api/Patient type POST
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllPatientQuery(FacilityId)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _mediator.Send(new GetPatientByIdQuery(FacilityId,id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PatientDto updatedPatient)
        {
            return Ok(await _mediator.Send(new UpdatePatientCommand(updatedPatient)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new DeletePatientByIdCommand(id,FacilityId)));
        }
    }
}