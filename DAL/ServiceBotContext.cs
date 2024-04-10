using DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public class ServiceBotContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=TestDB;Username=postgres;Password=1Qa2wS_3_4RF");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<IncidentReport> IncidentReports { get; set; }
        public virtual DbSet<RequestsForDays> RequestsForDays { get; set; } 

    }
}
