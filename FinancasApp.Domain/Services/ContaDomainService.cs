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
    public class ContaDomainService : IContaDomainService
    {
        private readonly IContaRepository? _contaRepository;

        public ContaDomainService(IContaRepository? contaRepository)
        {
            _contaRepository = contaRepository;
        }


        public void Excluir(Guid id, Guid usuarioId)
        {
            //recuperar a conta no banco de dados
            var conta = _contaRepository?.GetById(id);

            //verificar se a conta foi encontrada e se pertence ao usuário
            if (conta != null && conta.UsuarioId == usuarioId)
                //excluindo a conta
                _contaRepository?.Delete(conta);
            else
                throw new ApplicationException("Conta inválida para exclusão.");
        }


        public List<Conta> Consultar(DateTime dataMin, DateTime dataMax, Guid usuarioId)
        {
            var qtdDias = (dataMax - dataMin).Days;
            if (qtdDias > 90)
                throw new ApplicationException("O período de datas para a consulta deve ser de no máximo 90 dias");

            return _contaRepository?.Get(dataMin, dataMax, usuarioId);

        }

        public Conta ObterPorId(Guid id, Guid usuarioId)
        {
            var conta = _contaRepository?.GetById(id);

            //verificar se a conta foi encontrada e se pertence ao usuário
            if (conta != null && conta.UsuarioId == usuarioId)
                return conta;
            else
                throw new ApplicationException("Conta inválida.");

        }

       
      

        public void Atualizar(Conta conta)
        {
            _contaRepository?.Update(conta);
        }

        public void Cadastrar(Conta conta)
        {
            _contaRepository?.Add(conta);
        }
    }
}
