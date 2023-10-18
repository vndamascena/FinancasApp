using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface para métodos de repositório de conta
    /// </summary>
    public interface IContaRepository : IBaseRepository<Conta>
    {
        List<Conta> Get(DateTime dataMin, DateTime dataMax, Guid usuarioId);
    }
}


