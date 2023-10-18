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
    /// Classe de mapeamento ORM para a entidade Categoria
    /// </summary>
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //nome da tabela
            builder.ToTable("CATEGORIA");

            //chave primária
            builder.HasKey(c => c.Id);

            //mapeamento dos campos
            builder.Property(c => c.Id)
                .HasColumnName("ID");

            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}



