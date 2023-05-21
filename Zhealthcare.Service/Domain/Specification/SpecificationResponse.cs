using Microsoft.Azure.CosmosRepository;
using Zhealthcare.Service.Domain.Entities;

namespace Zhealthcare.Service.Domain.Specification
{
    public class PatientSpecificationResult : IQueryResult<Patient>
    {
        public IReadOnlyList<Patient> Items { get; set;} = new List<Patient>();

        public double Charge { get; set; }
    }
}
