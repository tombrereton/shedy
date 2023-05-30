using System.Text.Json;

namespace Shedy.Api.Middleware
{
    public class ErrorDetails
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public string? Details { get; set; }
        public string? Errors { get; set; }
        

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}