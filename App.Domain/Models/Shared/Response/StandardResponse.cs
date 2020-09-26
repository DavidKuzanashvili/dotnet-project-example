namespace App.Domain.Models.Shared.Response
{
    public class StandardResponse<T> where T : class, new()
    {
        public T Data { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }
}
