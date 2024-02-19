﻿// <auto-generated />
using System;
using GerenciamentoFrotaVeiculo.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GerenciamentoFrotaVeiculo.Context.Migrations
{
    [DbContext(typeof(ECommerceContext))]
    partial class ECommerceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GerenciamentoFrotaVeiculo.Models.Colaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colaboradores", (string)null);
                });

            modelBuilder.Entity("GerenciamentoFrotaVeiculo.Models.ColaboradorVeiculo", b =>
                {
                    b.Property<int>("ColaboradorId")
                        .HasColumnType("int");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DataInicioVinculo")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ColaboradorId", "VeiculoId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("ColaboradoresVeiculos", (string)null);
                });

            modelBuilder.Entity("GerenciamentoFrotaVeiculo.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Ano")
                        .HasColumnType("datetime2");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Veiculos", (string)null);
                });

            modelBuilder.Entity("GerenciamentoFrotaVeiculo.Models.ColaboradorVeiculo", b =>
                {
                    b.HasOne("GerenciamentoFrotaVeiculo.Models.Colaborador", "Colaborador")
                        .WithMany("ColaboradoresVeiculos")
                        .HasForeignKey("ColaboradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerenciamentoFrotaVeiculo.Models.Veiculo", "Veiculo")
                        .WithMany("ColaboradoresVeiculos")
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colaborador");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("GerenciamentoFrotaVeiculo.Models.Colaborador", b =>
                {
                    b.Navigation("ColaboradoresVeiculos");
                });

            modelBuilder.Entity("GerenciamentoFrotaVeiculo.Models.Veiculo", b =>
                {
                    b.Navigation("ColaboradoresVeiculos");
                });
#pragma warning restore 612, 618
        }
    }
}
