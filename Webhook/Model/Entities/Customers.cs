namespace Webhook.Model.Entities
{
    public class Customers
    {
        public int Id { get; set; }
        public string? Mobile { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool  Isdeleted { get; set; }
    }
}
