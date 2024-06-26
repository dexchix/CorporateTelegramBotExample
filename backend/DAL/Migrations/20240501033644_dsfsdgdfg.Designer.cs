﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ServiceBotContext))]
    [Migration("20240501033644_dsfsdgdfg")]
    partial class dsfsdgdfg
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Employe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Department")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<bool?>("IsAutorized")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<long?>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<string>("TelegramLogin")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Employes");
                });

            modelBuilder.Entity("DAL.Models.IncidentReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("EmployeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("IncidentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Number")
                        .HasColumnType("integer");

                    b.Property<long?>("TelegramChatId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EmployeId");

                    b.ToTable("IncidentReports");
                });

            modelBuilder.Entity("DAL.Models.RequestsForDays", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("EmployeFullName")
                        .HasColumnType("text");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Number")
                        .HasColumnType("integer");

                    b.Property<int?>("RequestStatus")
                        .HasColumnType("integer");

                    b.Property<int?>("RequestType")
                        .HasColumnType("integer");

                    b.Property<string>("Responce")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("TelegramChatId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("RequestsForDays");
                });

            modelBuilder.Entity("DAL.Models.IncidentReport", b =>
                {
                    b.HasOne("DAL.Models.Employe", "Employe")
                        .WithMany()
                        .HasForeignKey("EmployeId");

                    b.Navigation("Employe");
                });

            modelBuilder.Entity("DAL.Models.RequestsForDays", b =>
                {
                    b.HasOne("DAL.Models.Employe", "Employee")
                        .WithMany("RequestsForDays")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DAL.Models.Employe", b =>
                {
                    b.Navigation("RequestsForDays");
                });
#pragma warning restore 612, 618
        }
    }
}
