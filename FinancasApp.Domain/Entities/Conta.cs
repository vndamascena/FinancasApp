using FinancasApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Entities
{
    /// <summary>
    /// Modelo de entidade para Conta
    /// </summary>
    public class Conta
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public TipoConta Tipo { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }

        //Conta TEM 1 Categoria
        public Categoria Categoria { get; set; }

        //Conta TEM 1 Usuário
        public Usuario Usuario { get; set; }
    }
}
