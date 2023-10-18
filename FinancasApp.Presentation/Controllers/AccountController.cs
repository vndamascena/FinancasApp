using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Presentation.Models.Account;
using FinancasApp.Presentation.Models.Usuarios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace FinancasApp.Presentation.Controllers
{
    /// <summary>
    /// Controlador para páginas da pasta /Account
    /// </summary>
    public class AccountController : Controller
    {
        //atributo
        private readonly IUsuarioDomainService? _usuarioDomainService;

        //construtor para inicialização do atributo (injeção de dependência)
        public AccountController(IUsuarioDomainService? usuarioDomainService)
        {
            _usuarioDomainService = usuarioDomainService;
        }

        /// <summary>
        /// Método para abrir a página /Account/Login
        /// </summary>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Método para capturar o SUBMIT POST do formulário
        /// </summary>
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            //verificando se todos os campos passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    //obter o usuário baseado no email e na senha
                    var usuario = _usuarioDomainService?.Autenticar(model.Email, model.Senha);

                    //armazenar os dados que serão gravados no cookie de autenticação
                    var usuarioViewModel = new UsuarioViewModel
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        DataHoraAcesso = DateTime.Now
                    };

                    //serializando os dados para o formato JSON
                    var jsonData = JsonConvert.SerializeObject(usuarioViewModel);

                    //definindo os dados para o cookie de autenticação
                    var claimsIdentity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, jsonData)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    //gravando o cookie de autenticação
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    //redirecionando para a página /Home/Index
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        /// <summary>
        /// Método para abrir a página /Account/Register
        /// </summary>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Método para capturar o SUBMIT POST do formulário
        /// </summary>
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            //verificando se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Email = model.Email,
                        Senha = model.Senha
                    };

                    _usuarioDomainService?.CriarUsuario(usuario);
                    ModelState.Clear(); //limpar os campos do formulário

                    TempData["MensagemSucesso"] = "Parabéns, sua conta de usuário foi criada com sucesso!";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        /// <summary>
        /// Método para abrir a página /Account/ForgotPassword
        /// </summary>
        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            
            //apagar o cookie de autenticação do usuário
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();

            //redirecionar o usuário de volta para a página de login
            return RedirectToAction("Login", "Account");
        }
    }
}



