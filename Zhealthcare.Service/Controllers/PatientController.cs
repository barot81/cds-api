using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Zhealthcare.Service.Application.Patients.Commands;
using Zhealthcare.Service.Application.Patients.Models;
using Zhealthcare.Service.Application.Patients.Queries;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Controllers
{
    [Route("api/")]
    [ApiController]
    [RequiredScope("patients.read")]
    [Authorize]
    public class PatientsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly UserContext _userContext;
        public PatientsController(IMediator mediator, UserContext userContext)
        {
            _mediator = mediator;
            _userContext = userContext;
        }


        // URL - https://localhost:44378/api/Patient type POST
        [HttpPost("{FacilityId}/patients")]
        public async Task<IActionResult> Create(string FacilityId, PatientDto PatientInfo)
        => Ok(await _mediator.Send(new CreatePatientCommand(FacilityId, PatientInfo)));


        [HttpGet("{FacilityId}/patients")]
        public async Task<IActionResult> GetAll(string FacilityId)
        => Ok(await _mediator.Send(new GetAllPatientsQuery(FacilityId)));
        

        [HttpGet("{FacilityId}/patientsInfo")]
        public async Task<IActionResult> GetPatientInfo(string FacilityId, [FromQuery] PageFilterModel pageFilterModel)
        => Ok(await _mediator.Send(new GetAllPatientsQueriesRequest(FacilityId, pageFilterModel)));
        

        [HttpGet("{FacilityId}/patients/{id}")]
        public async Task<IActionResult> GetById(string FacilityId, Guid id)
        => Ok(await _mediator.Send(new GetPatientsByIdQuery(FacilityId, id)));
        

        [HttpPut("{FacilityId}/patients/{id}")]
        public async Task<IActionResult> Update(string FacilityId, Guid Id, [FromBody] PatientUpdateDto updatedPatient)
        => Ok(await _mediator.Send(new UpdatePatientCommand(Id.ToString(), FacilityId, updatedPatient, _userContext.Name)));
        

        [HttpPut("{FacilityId}/patients/{id}/comments")]
        public async Task<IActionResult> UpdateComments(string FacilityId, Guid Id, [FromBody] PatientCommentUpdateDto updatedPatientComment)
        => Ok(await _mediator.Send(new UpdatePatientCommentRequest(Id, FacilityId)
            {
                ReviewStatus = updatedPatientComment.ReviewStatus,
                GeneralComments = updatedPatientComment.GeneralComment
            }));
        

        [HttpPut("{FacilityId}/patients/{id}/reviewStatus")]
        public async Task<IActionResult> UpdateReviewStatus(string FacilityId, Guid Id, [FromBody] PatientReviewStatusUpdateDto updatedReviewStatusComment)
        => Ok(await _mediator.Send(
            new UpdatePatientReviewStatusRequest(Id, FacilityId)
            {
                ReviewStatus = updatedReviewStatusComment.ReviewStatus
        }));
    

        [HttpDelete("{FacilityId}/patients/{id}")]
        public async Task<IActionResult> Delete(string FacilityId, Guid id, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new DeletePatientByIdCommand(FacilityId, id), cancellationToken));

    }
}