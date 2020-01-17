﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SCA.Service.Maintenance.Data;

namespace SCA.Service.Maintenance.Migrations
{
    [DbContext(typeof(MaintenanceContext))]
    partial class MaintenanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SCA.Shared.Entities.Maintenance.Manutencao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataAgendamento")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataFimManutencao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInicioManutencao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DescricaoAgendamento")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DescricaoManutencao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("InsumoDesc")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("InsumoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PrevisaoManutencao")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Manutencao");
                });
#pragma warning restore 612, 618
        }
    }
}
