using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Entities
{
    /// <summary>
    /// Modelo de entidade para Usuário
    /// </summary>
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        //Usuário TEM Muitas Contas
        public List<Conta> Contas { get; set; }
    }
}



