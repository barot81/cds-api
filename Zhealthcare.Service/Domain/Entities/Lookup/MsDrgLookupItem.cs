
using Zhealthcare.Service.Domain.Entities.Lookup;

namespace Zhealthcare.Service.Domain.Entities.Drg
{
    public class MsDrgLookupItem : ILookupItem
    {
        public string DrgNo { get; set; } = string.Empty;
        public string IsPostAcuteDrg { get; set; } = string.Empty;
        public string IsSpecialPayDrg { get; set; } = string.Empty;
        public string Mdc { get; set; } = string.Empty;
        public string DrgType { get; set; } = string.Empty;
        public string DrgTitle { get; set; } = string.Empty;
        public string Weights { get; set; } = string.Empty;
        public string GeometricMeanLos { get; set; } = string.Empty;
        public string ArithmeticMeanLos { get; set; } = string.Empty;
    }
}
