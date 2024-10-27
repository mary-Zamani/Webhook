using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webhook.Model.Entities;

namespace Webhook.Model.Entities.Configuration
{
    public class ReceivedMessagesConfiguration
    {
        public void Configure(EntityTypeBuilder<ReceivedMessages> builder)
        {
            builder.HasKey(u => u.Id);
           // builder.HasMany(u => u.Roles)
           //.WithOne()
           //.HasForeignKey(r => r.Id)
           //.OnDelete(DeleteBehavior.Restrict);


        }
    }
}
