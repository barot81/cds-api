namespace Zhealthcare.Service.Domain.Entities
{
    public class GeneralComment
    {
        public string Comments { get; set; } = string.Empty;

        public string AddedBy { get; set; } = string.Empty;

        public DateTime? AddedOn { get; set; }
    }

}
