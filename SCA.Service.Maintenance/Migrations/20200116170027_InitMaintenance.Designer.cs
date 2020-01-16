﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SCA.Service.Maintenance.Data;

namespace SCA.Service.Maintenance.Migrations
{
    [DbContext(typeof(MaintenanceContext))]
    [Migration("20200116170027_InitMaintenance")]
    partial class InitMaintenance
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SCA.Shared.Entities.Inputs.Insumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TipoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.HasIndex("TipoId");

                    b.ToTable("Insumo");
                });

            modelBuilder.Entity("SCA.Shared.Entities.Inputs.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("SCA.Shared.Entities.Inputs.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Tipo");
                });

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

                    b.Property<int>("InsumoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PrevisaoManutencao")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InsumoId");

                    b.ToTable("Manutencao");
                });

            modelBuilder.Entity("SCA.Shared.Entities.Inputs.Insumo", b =>
                {
                    b.HasOne("SCA.Shared.Entities.Inputs.Marca", "Marca")
                        .WithMany("Insumos")
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SCA.Shared.Entities.Inputs.Tipo", "Tipo")
                        .WithMany("Insumos")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SCA.Shared.Entities.Maintenance.Manutencao", b =>
                {
                    b.HasOne("SCA.Shared.Entities.Inputs.Insumo", "Insumo")
                        .WithMany()
                        .HasForeignKey("InsumoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}