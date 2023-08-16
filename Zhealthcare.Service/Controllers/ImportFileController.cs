using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Zhealthcare.Service.Application.ImportFile;
using Zhealthcare.Service.Application.Patients.Commands;
using Zhealthcare.Service.Application.Patients.Models;

namespace Zhealthcare.Service.Controllers
{
    [Route("api/")]
    [ApiController]
    //[Authorize]
    //[RequiredScope("patients.read")]
    [AllowAnonymous]
    public class ImportFileController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ImportFileController(IMediator mediator)
        => _mediator = mediator;


        // URL - https://localhost:44378/api/Patient type POST
        [HttpPost("{FacilityId}/importFile/Patients")]
        public async Task<IActionResult> ImportPatients(string FacilityId, string FileName)
        => Ok(await _mediator.Send(new PatientsUploadCommand(FacilityId, FileName)));
        
    }
}
