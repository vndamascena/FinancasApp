using FinancasApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento ORM para a entidade Conta
    /// </summary>
    public class ContaMap : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            //nome da tabela
            builder.ToTable("CONTA");

            //chave primária
            builder.HasKey(c => c.Id);

            //mapeamento dos campos
            builder.Property(c => c.Id)
                .HasColumnName("ID");

            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.Valor)
                .HasColumnName("VALOR")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.Data)
                .HasColumnName("DATA")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.Tipo)
                .HasColumnName("TIPO")
                .IsRequired();

            builder.Property(c => c.CategoriaId)
                .HasColumnName("CATEGORIAID")
                .IsRequired();

            builder.Property(c => c.UsuarioId)
                .HasColumnName("USUARIOID")
                .IsRequired();

            builder.HasOne(c => c.Categoria) //Conta TEM 1 Categoria
                .WithMany(c => c.Contas) //Categoria TEM MUITAS Contas
                .HasForeignKey(c => c.CategoriaId) //Chave estrangeira
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Usuario) //Conta TEM 1 Usuario
                .WithMany(u => u.Contas) //Usuario TEM MUITAS Contas
                .HasForeignKey(c => c.UsuarioId) //Chave estrangeira
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}



