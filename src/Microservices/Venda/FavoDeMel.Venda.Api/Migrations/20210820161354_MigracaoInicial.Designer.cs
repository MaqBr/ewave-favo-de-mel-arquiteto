// <auto-generated />
using System;
using FavoDeMel.Venda.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FavoDeMel.Venda.Api.Migrations
{
    [DbContext(typeof(VendaDbContext))]
    [Migration("20210820161354_MigracaoInicial")]
    partial class MigracaoInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.Comanda", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ComandaStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("MesaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("VoucherUtilizado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MesaId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Comandas");
                });

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.ComandaItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComandaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ItemStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProdutoNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ComandaId");

                    b.ToTable("ComandaItems");
                });

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.Mesa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUtilizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Percentual")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("TipoDescontoVoucher")
                        .HasColumnType("int");

                    b.Property<bool>("Utilizado")
                        .HasColumnType("bit");

                    b.Property<decimal?>("ValorDesconto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.Comanda", b =>
                {
                    b.HasOne("FavoDeMel.Venda.Domain.Models.Mesa", "Mesa")
                        .WithMany("Comandas")
                        .HasForeignKey("MesaId");

                    b.HasOne("FavoDeMel.Venda.Domain.Models.Voucher", "Voucher")
                        .WithMany("Comandas")
                        .HasForeignKey("VoucherId");

                    b.Navigation("Mesa");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.ComandaItem", b =>
                {
                    b.HasOne("FavoDeMel.Venda.Domain.Models.Comanda", "Comanda")
                        .WithMany("ComandaItems")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comanda");
                });

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.Comanda", b =>
                {
                    b.Navigation("ComandaItems");
                });

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.Mesa", b =>
                {
                    b.Navigation("Comandas");
                });

            modelBuilder.Entity("FavoDeMel.Venda.Domain.Models.Voucher", b =>
                {
                    b.Navigation("Comandas");
                });
#pragma warning restore 612, 618
        }
    }
}
