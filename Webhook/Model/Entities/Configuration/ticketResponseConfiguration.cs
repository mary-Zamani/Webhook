using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webhook.Model.Entities;

namespace Webhook.Model.Entities.Configuration
{
    public class ticketResponseConfiguration
    {
        public void Configure(EntityTypeBuilder<ticketResponse> builder)
        {
            builder.HasKey(u => u.s_id);
         
        }
    }
}
