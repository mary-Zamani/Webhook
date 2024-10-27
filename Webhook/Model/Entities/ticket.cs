using System.Text.Json.Serialization;
namespace Webhook.Model.Entities
{
    public class ticket
    {
        public int s_id { get; set; }
        public string? Subject { get; set; }
        public string? Discription { get; set; }
        public int? Status { get; set; }
        public int? UserId { get; set; }
        public int? Asign { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? PcreateDate { get; set; }
        public string? PupdateDate { get; set; }
        public string? FileName { get; set; }
        public int? uc { get; set; }
        public int? ReciveUc { get; set; }
        public int? SendUc { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerMobile { get; set; }
        public int? CustomerAsign { get; set; }
        public int? CustomerUc { get; set; }
        public int? CustomerReciveUc { get; set; }
        public int? CustomerSendUc { get; set; }
    }
}
