using System.Text.Json.Serialization;
namespace Webhook.Model.Entities
{
    public class webhookError
    {
      
        public int Id { get; set; }
        public string? json { get; set; }
        public DateTime createdate { get; set; }
        public string? error { get; set; }
        
      
        
    }
}
