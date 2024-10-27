using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webhook.Model.Entities;

namespace Webhook.Model.Entities.Configuration
{
    public class ticketConfiguration
    {
        public void Configure(EntityTypeBuilder<ticket> builder)
        {
            builder.HasKey(u => u.s_id);
           // builder.HasMany(u => u.Roles)
           //.WithOne()
           //.HasForeignKey(r => r.Id)
           //.OnDelete(DeleteBehavior.Restrict);


        }
    }
}
