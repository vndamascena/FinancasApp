using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Repositories
{
    public class ContaRepository
        : BaseRepository<Conta>, IContaRepository
    {
        public List<Conta> Get(DateTime dataMin, DateTime dataMax, Guid usuarioId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Conta>()
                    .Include(c => c.Categoria) //JOIN
                    .Where(c => c.Data >= dataMin && c.Data <= dataMax && c.UsuarioId == usuarioId)
                    .OrderBy(c => c.Data)
                    .ToList();
            }
        }
    }
}



