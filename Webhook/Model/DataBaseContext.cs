using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection;
using Webhook.Model.Entities;

namespace HangfireApp.Models
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<ReceivedMessages> ReceivedMessages { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<ticket> ticket { get; set; }
        public DbSet<ticketResponse> ticketResponse { get; set; }
        public DbSet<waapi> waapi { get; set; }
        public DbSet<webhookError> webhookError { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ticket>().HasKey(t => t.s_id);
            modelBuilder.Entity<Customers>().HasKey(t => t.Id);
            modelBuilder.Entity<ticketResponse>().HasKey(t => t.s_id); 
            modelBuilder.Entity<waapi>().HasKey(t => t.Id); 
            modelBuilder.Entity<webhookError>().HasKey(t => t.Id); 
        }


    }
}
