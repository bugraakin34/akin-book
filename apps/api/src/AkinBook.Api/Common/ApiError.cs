namespace AkinBook.Api.Common
{
    public sealed class ApiError
    {
        public string Type { get; set; } = default!;
        public string Message { get; set; } = default!;
        public object? Details { get; set; }
        public string? TraceId { get; set; }
    }
}
