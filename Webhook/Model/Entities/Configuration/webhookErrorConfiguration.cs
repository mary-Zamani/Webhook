using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webhook.Model.Entities;

namespace Webhook.Model.Entities.Configuration
{
    public class webhookErrorConfiguration
    {
        public void Configure(EntityTypeBuilder<webhookError> builder)
        {
            builder.HasKey(u => u.Id);
 
        }
    }
}
