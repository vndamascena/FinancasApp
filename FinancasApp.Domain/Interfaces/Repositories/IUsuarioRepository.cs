using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface para métodos de repositório de usuário
    /// </summary>
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario? Get(string email);
        Usuario? Get(string email, string senha);

    }
}
