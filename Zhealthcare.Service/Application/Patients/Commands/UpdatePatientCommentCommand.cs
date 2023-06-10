using MediatR;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record UpdatePatientCommentRequest(Guid Id, string FacilityId) : IRequest<Guid>
    {
        public GeneralComment GeneralComments { get; set; } = new GeneralComment();

        public string ReviewStatus { get; set; } = string.Empty;

    }

}
