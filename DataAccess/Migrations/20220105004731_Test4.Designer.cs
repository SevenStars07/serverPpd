﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(SpectacolDbContext))]
    [Migration("20220105004731_Test4")]
    partial class Test4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Model.Sala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("NrLocuri")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sali");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NrLocuri = 100
                        });
                });

            modelBuilder.Entity("Model.Spectacol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataSpectacol")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocuriVandute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PretBilet")
                        .HasColumnType("int");

                    b.Property<int>("SalaId")
                        .HasColumnType("int");

                    b.Property<int>("Sold")
                        .HasColumnType("int");

                    b.Property<string>("Titlu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SalaId");

                    b.ToTable("Spectacols");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataSpectacol = new DateTime(2021, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LocuriVandute = "",
                            PretBilet = 100,
                            SalaId = 1,
                            Sold = 0,
                            Titlu = "S1"
                        },
                        new
                        {
                            Id = 2,
                            DataSpectacol = new DateTime(2021, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LocuriVandute = "",
                            PretBilet = 200,
                            SalaId = 1,
                            Sold = 0,
                            Titlu = "S2"
                        },
                        new
                        {
                            Id = 3,
                            DataSpectacol = new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LocuriVandute = "",
                            PretBilet = 150,
                            SalaId = 1,
                            Sold = 0,
                            Titlu = "S3"
                        });
                });

            modelBuilder.Entity("Model.Vanzare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataVanzare")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocuriVandute")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NrBileteVandute")
                        .HasColumnType("int");

                    b.Property<int?>("SalaId")
                        .HasColumnType("int");

                    b.Property<int>("SpectacolId")
                        .HasColumnType("int");

                    b.Property<int>("Suma")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalaId");

                    b.HasIndex("SpectacolId");

                    b.ToTable("Vanzari");
                });

            modelBuilder.Entity("Model.Spectacol", b =>
                {
                    b.HasOne("Model.Sala", "Sala")
                        .WithMany("Spectacole")
                        .HasForeignKey("SalaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sala");
                });

            modelBuilder.Entity("Model.Vanzare", b =>
                {
                    b.HasOne("Model.Sala", null)
                        .WithMany("Vanzari")
                        .HasForeignKey("SalaId");

                    b.HasOne("Model.Spectacol", "Spectacol")
                        .WithMany()
                        .HasForeignKey("SpectacolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spectacol");
                });

            modelBuilder.Entity("Model.Sala", b =>
                {
                    b.Navigation("Spectacole");

                    b.Navigation("Vanzari");
                });
#pragma warning restore 612, 618
        }
    }
}
