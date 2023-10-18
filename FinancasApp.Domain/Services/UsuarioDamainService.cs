using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Helpers;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributo
        private readonly IUsuarioRepository? _usuarioRepository;

        //construtor utilizado para inicializar o atributo (injeção de dependência)
        public UsuarioDomainService(IUsuarioRepository? usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void CriarUsuario(Usuario usuario)
        {
            //verificar se já existe um usuário cadastrado com o email informado
            if (_usuarioRepository?.Get(usuario.Email) != null)
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");

            //criptografar a senha do usuário
            usuario.Senha = SHA1Helper.Encrypt(usuario.Senha);

            //gravar o usuário no banco de dados
            _usuarioRepository?.Add(usuario);
        }

        public Usuario Autenticar(string email, string senha)
        {
            //consultar o usuário no banco de dados através do email e da senha
            var usuario = _usuarioRepository?.Get(email, SHA1Helper.Encrypt(senha));

            //verificar se o usuário não foi encontrado
            if (usuario == null)
                throw new ApplicationException("Acesso negado. Usuário inválido.");

            //retornar os dados do usuário
            return usuario;
        }
    }
}



