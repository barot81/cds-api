using MediatR;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.ImportFile
{
    public record PatientsUploadCommand(string FacilityId, string FileName) : IRequest<ImportFileResponse>;
}
