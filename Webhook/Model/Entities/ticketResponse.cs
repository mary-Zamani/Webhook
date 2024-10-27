namespace Webhook.Model.Entities
{
    public class ticketResponse
    {
        public int s_id { get; set; }
        public int? TicketId { get; set; }
        public int? UserId { get; set; }
        public int? CustomerId { get; set; }
        public int? RefId { get; set; }
        public string? Response { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateDate { get; set; }
        public bool Isdeleted { get; set; } = false;
        public string? PcreateDate { get; set; }
    }
}
