
namespace Zhealthcare.Service.Domain.Entities.Drg
{

    public class AprDrgLookupItem : ILookupItem {
        public string DrgNo { get; set; } = string.Empty;
        public string DrgDescription { get; set; } = string.Empty;
        public string NationalAverageLos { get; set; } = string.Empty;
        public string RelativeWeight { get; set; } = string.Empty;
        public string AdultMedicaidCareCategory { get; set; } = string.Empty;
        public string PediatricMedicaidCareCategory { get; set; } = string.Empty;
    }
}
