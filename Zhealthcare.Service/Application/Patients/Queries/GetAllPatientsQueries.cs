using MediatR;
using Zhealthcare.Service.Models;

namespace Zhealthcare.Service.Application.Patients.Queries
{
    public record  GetAllPatientsQueriesRequest(string FacilityId, PageFilterModel FilterModel) : IRequest<PageResponseModel>;
}
