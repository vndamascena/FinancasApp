using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Entities
{
    /// <summary>
    /// Modelo de entidade para Categoria
    /// </summary>
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        //Categoria TEM Muitas Contas
        public List<Conta> Contas { get; set; }
    }
}
