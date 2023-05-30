using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zhealthcare.Service.Application.Dashboard;
using Zhealthcare.Service.Application.Lookups;
using Zhealthcare.Service.Domain.Entities.Drg;
using Zhealthcare.Service.Domain.Entities.Lookup;

namespace Zhealthcare.Service.Controllers
{
    [Route("api/")]
    [ApiController]

    public class LookupsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LookupsController(IMediator mediator)
        => _mediator = mediator;

        [HttpGet("Lookups/MsDrg")]
        public async Task<IActionResult> GetMsDrgLookup()
        {
            return Ok(await _mediator.Send(new GetMsDrgLookupRequest()));
        }

        [HttpGet("Lookups/AprDrg")]
        public async Task<IActionResult> GetAprDrgLookup()
        {
            return Ok(await _mediator.Send(new GetAprDrgLookupRequest()));
        }

        [HttpGet("Lookups/QueryDiagnosis")]
        public async Task<IActionResult> GetDiagnosisLookup()
        {
            return Ok(await _mediator.Send(new GetDiagnosisLookupRequest()));
        }

        [HttpGet("Lookups/ReimursementType")]
        public async Task<IActionResult> GetReimbursementTypeLookup()
        {
            return Ok(await _mediator.Send(new GetReimbursementTypeLookupRequest()));
        }
    }
}
