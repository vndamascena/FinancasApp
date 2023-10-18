using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Services
{
    public class CategoriaDomainService : ICategoriadomainService
    {
        private readonly ICategoriaRepository? _categoriaRepository;

        public CategoriaDomainService(ICategoriaRepository? categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public List<Categoria> Consultar()
        {
            return _categoriaRepository?.GetAll();
        }
    }
}
