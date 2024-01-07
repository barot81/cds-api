using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Zhealthcare.Service.Application.ImportFile;

namespace Zhealthcare.Service.Controllers
{
    [Route("api/")]
    [ApiController]
    [Authorize]
    [RequiredScope("patients.read")]
    public class ImportFileController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ImportFileController(IMediator mediator)
        => _mediator = mediator;


        // URL - https://localhost:44378/api/Patient type POST
        [HttpPost("{FacilityId}/importFile/Patients")]
        public async Task<IActionResult> ImportPatients(string FacilityId, string FileName)
        => Ok(await _mediator.Send(new PatientsUploadCommand(FacilityId, FileName)));

        [HttpPost("{FacilityId}/File/Patients")]
        public async Task<IActionResult> ImportPatientsFromExcel(string FacilityId, [FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Invalid file");

                var result = await _mediator.Send(new PatientsImportFromExcelCommand(FacilityId, file));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
