
using Refit;
using Zhealthcare.Service.Application.Patients.Models;

namespace Zhealthcare.Utility.Services
{
    internal interface IPatientService
    {
        [Get("/api/{FacilityId}/patients")]
        Task<dynamic> AddPatient(string FacilityId, [Body] PatientDto PatientDto);
    }
}
