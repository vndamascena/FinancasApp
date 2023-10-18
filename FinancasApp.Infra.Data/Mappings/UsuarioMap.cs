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
    /// Classe de mapeamento ORM para a entidade Usuario
    /// </summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //nome da tabela
            builder.ToTable("USUARIO");

            //chave primária
            builder.HasKey(u => u.Id);

            //mapeamento dos campos
            builder.Property(u => u.Id)
                .HasColumnName("ID");

            builder.Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(40)
                .IsRequired();
        }
    }
}



