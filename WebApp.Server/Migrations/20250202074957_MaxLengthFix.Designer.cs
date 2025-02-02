﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApp.Server.Data;

#nullable disable

namespace WebApp.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250202074957_MaxLengthFix")]
    partial class MaxLengthFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MechanicRepair", b =>
                {
                    b.Property<int>("MechanicsId")
                        .HasColumnType("integer");

                    b.Property<int>("RepairsId")
                        .HasColumnType("integer");

                    b.HasKey("MechanicsId", "RepairsId");

                    b.HasIndex("RepairsId");

                    b.ToTable("MechanicRepair");
                });

            modelBuilder.Entity("WebApp.Server.Data.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("WebApp.Server.Data.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HashedAccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedRefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("WebApp.Server.Data.Mechanic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Mechanics");
                });

            modelBuilder.Entity("WebApp.Server.Data.Part", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("RepairId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RepairId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("WebApp.Server.Data.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("RepairDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RepairOrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RepairOrderId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("WebApp.Server.Data.RepairOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("RepairOrders");
                });

            modelBuilder.Entity("WebApp.Server.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApp.Server.Data.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("MechanicRepair", b =>
                {
                    b.HasOne("WebApp.Server.Data.Mechanic", null)
                        .WithMany()
                        .HasForeignKey("MechanicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApp.Server.Data.Repair", null)
                        .WithMany()
                        .HasForeignKey("RepairsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Server.Data.Part", b =>
                {
                    b.HasOne("WebApp.Server.Data.Repair", null)
                        .WithMany("Parts")
                        .HasForeignKey("RepairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Server.Data.Repair", b =>
                {
                    b.HasOne("WebApp.Server.Data.RepairOrder", null)
                        .WithMany("Repairs")
                        .HasForeignKey("RepairOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Server.Data.RepairOrder", b =>
                {
                    b.HasOne("WebApp.Server.Data.Vehicle", null)
                        .WithMany("RepairOrders")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Server.Data.Vehicle", b =>
                {
                    b.HasOne("WebApp.Server.Data.Client", null)
                        .WithMany("Vehicles")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApp.Server.Data.Client", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("WebApp.Server.Data.Repair", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("WebApp.Server.Data.RepairOrder", b =>
                {
                    b.Navigation("Repairs");
                });

            modelBuilder.Entity("WebApp.Server.Data.Vehicle", b =>
                {
                    b.Navigation("RepairOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
