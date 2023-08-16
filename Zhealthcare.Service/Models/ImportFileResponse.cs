namespace Zhealthcare.Service.Models
{
    public record ImportFileResponse
    {
        public ImportFileResponse(bool isSuccess, IEnumerable<ErrorDetail> errorDetails)
        {
            IsSuccess = isSuccess;
            Errors = errorDetails ?? Enumerable.Empty<ErrorDetail>();
        }
        public bool IsSuccess { get; set; }

        public IEnumerable<ErrorDetail> Errors { get; set; } = Enumerable.Empty<ErrorDetail>();
    }

    public record ErrorDetail
    {
        public string Error { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
