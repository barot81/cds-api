using MediatR;
namespace Zhealthcare.Service.Application.Patients.Commands
{
    public record UpdatePatientReviewStatusRequest(Guid Id, string FacilityId) : IRequest<Guid>
    {
        public string ReviewStatus { get; set; } = string.Empty;
    }

}
