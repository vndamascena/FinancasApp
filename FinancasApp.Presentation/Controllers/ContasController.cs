using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Enums;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Domain.Services;
using FinancasApp.Presentation.Models.Contas;
using FinancasApp.Presentation.Models.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Reflection.Metadata.Ecma335;

namespace FinancasApp.Presentation.Controllers
{
    /// <summary>
    /// Controlador para páginas da pasta /Contas
    /// </summary>
    [Authorize]
    public class ContasController : Controller
    {
        //atributos
        private readonly ICategoriadomainService _categoriaDomainService;
        private readonly IContaDomainService? _contaDomainService;

        //construtor para injeção de dependência
        public ContasController(ICategoriadomainService? categoriaDomainService, IContaDomainService? contaDomainService)
        {
            _categoriaDomainService = categoriaDomainService;
            _contaDomainService = contaDomainService;
        }

        /// <summary>
        /// Método para abrir a página /Contas/Cadastro
        /// </summary>
        public IActionResult Cadastro()
        {
            var model = new ContasCadastroViewModel
            {
                Categorias = ObterCategorias()
            };

            return View(model);
        }

        /// <summary>
        /// Método para processar o SUBMIT POST
        /// </summary>
        [HttpPost]
        public IActionResult Cadastro(ContasCadastroViewModel model)
        {
            //Verificando se todos os campos passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Data = model.Data.Value,
                        Valor = model.Valor.Value,
                        Tipo = (TipoConta)model.Tipo.Value,
                        Descricao = model.Descricao,
                        CategoriaId = model.CategoriaId.Value,
                        UsuarioId = UsuarioAutenticado.Id
                    };

                    _contaDomainService?.Cadastrar(conta);

                    ModelState.Clear();
                    model = new ContasCadastroViewModel();

                    TempData["MensagemSucesso"] = $"Conta '{conta.Nome}', cadastrada com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            model.Categorias = ObterCategorias();
            return View(model);
        }

        /// <summary>
        /// Método para abrir a página /Contas/Consulta
        /// </summary>
        public IActionResult Consulta()
        {
            //verificar se existem datas selecionadas em sessão
            if (HttpContext.Session.GetString("DataMin") != null
                && HttpContext.Session.GetString("DataMax") != null)
            {
                var dataMin = DateTime.Parse(HttpContext.Session.GetString("DataMin"));
                var dataMax = DateTime.Parse(HttpContext.Session.GetString("DataMax"));

                try
                {
                    var contas = _contaDomainService?.Consultar(dataMin, dataMax, UsuarioAutenticado.Id);

                    //enviando os dados para a página
                    var model = new ContasConsultaViewModel
                    {
                        DataMin = dataMin,
                        DataMax = dataMax,
                        Contas = contas
                    };

                    return View(model);
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        /// <summary>
        /// Método para processar o SUBMIT POST
        /// </summary>
        [HttpPost]
        public IActionResult Consulta(ContasConsultaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //consultando as contas
                    var contas = _contaDomainService?.Consultar
                        (model.DataMin.Value, model.DataMax.Value, UsuarioAutenticado.Id);

                    model.Contas = contas;

                    //guardar as datas selecionadas em sessão
                    HttpContext.Session.SetString("DataMin", model.DataMin.Value.ToString());
                    HttpContext.Session.SetString("DataMax", model.DataMax.Value.ToString());
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            //devolvendo o objeto 'model' para a página
            return View(model);
        }

        /// <summary>
        /// Método para abrir a página /Contas/Edicao
        /// </summary>
        public IActionResult Edicao(Guid id)
        {
            var model = new ContasEdicaoViewModel();

            try
            {
                //consultar a conta através do ID
                var conta = _contaDomainService?.ObterPorId(id, UsuarioAutenticado.Id);

                //preenchando o objeto 'model' com os dados da conta
                model.Id = conta.Id;
                model.Nome = conta.Nome;
                model.Data = conta.Data;
                model.Valor = conta.Valor;
                model.Descricao = conta.Descricao;
                model.Tipo = (int)conta.Tipo;
                model.CategoriaId = conta.CategoriaId;
                model.Categorias = ObterCategorias();
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult Edicao(ContasEdicaoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var conta = new Conta
                    {
                        Id = model.Id.Value,
                        Nome = model.Nome,
                        Data = model.Data.Value,
                        Valor = model.Valor.Value,
                        Descricao = model.Descricao,
                        Tipo = (TipoConta)model.Tipo,
                        CategoriaId = model.CategoriaId.Value,
                        UsuarioId = UsuarioAutenticado.Id
                    };

                    _contaDomainService?.Atualizar(conta);
                    TempData["MensagemSucesso"] = $"Conta '{conta.Nome}', atualizada com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            model.Categorias = ObterCategorias();
            return View(model);
        }



        /// <summary>
        /// Método para executar a exclusão da conta
        /// </summary>
        public IActionResult Exclusao(Guid id)
        {
            try
            {
                _contaDomainService?.Excluir(id, UsuarioAutenticado.Id);
                TempData["MensagemSucesso"] = "Conta excluída com sucesso.";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        private List<SelectListItem> ObterCategorias()
        {
            var categorias = new List<SelectListItem>();
            foreach (var item in _categoriaDomainService?.Consultar())
            {
                categorias.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Nome
                });
            }

            return categorias;
        }

        /// <summary>
        /// Método para retornar o usuário autenticado
        /// </summary>
        private UsuarioViewModel UsuarioAutenticado
            => JsonConvert.DeserializeObject<UsuarioViewModel>(User.Identity.Name);
    }
}



