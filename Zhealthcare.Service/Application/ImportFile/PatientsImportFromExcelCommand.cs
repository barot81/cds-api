using MediatR;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.ImportFile
{
    public record PatientsImportFromExcelCommand(string FacilityId, IFormFile File) : IRequest<ImportFileResponse>;
}
