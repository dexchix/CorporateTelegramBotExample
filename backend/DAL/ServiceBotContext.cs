﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace DAL
{
    public class ServiceBotContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                 => optionsBuilder
                .UseLazyLoadingProxies()
                .UseNpgsql("");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<IncidentReport> IncidentReports { get; set; }
        public virtual DbSet<RequestsForDays> RequestsForDays { get; set; }
    }
}
