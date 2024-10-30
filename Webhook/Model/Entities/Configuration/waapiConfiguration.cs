using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webhook.Model.Entities;

namespace Webhook.Model.Entities.Configuration
{
    public class waapiConfiguration
    {
        public void Configure(EntityTypeBuilder<waapi> builder)
        {
            builder.HasKey(u => u.Id);
 
        }
    }
}
