using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Services
{
    public interface IContaDomainService
    {
        void Cadastrar(Conta conta);
        void Excluir(Guid id, Guid usuarioId);
        void Atualizar(Conta conta);
        List<Conta> Consultar(DateTime dataMin, DateTime dataMax, Guid usuarioId);
        Conta ObterPorId(Guid id, Guid usuarioId);
    }
}



