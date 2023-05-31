using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zhealthcare.Service.Application.Dashboard;

namespace Zhealthcare.Service.Controllers
{
    [Route("api/")]
    [ApiController]

    public class FacilityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FacilityController(IMediator mediator)
        => _mediator = mediator;

        [HttpGet("Facilities")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllFacilitiesQuery()));
        }

        [HttpGet("Facilities/{facilityId}/statuses")]
        public async Task<IActionResult> GetStatusesByFacility(string facilityId)
        {
            return Ok(await _mediator.Send(new GetAllStatusCountsQuery(facilityId)));
        }

        [HttpGet("Facilities/{facilityId}/Physicians")]
        public async Task<IActionResult> GetPhysiciansByFacility(string facilityId)
        {
            return Ok(await _mediator.Send(new GetPhysiciansQuery(facilityId)));
        }


    }
}
