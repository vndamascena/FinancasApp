using FinancasApp.Domain.Enums;
using FinancasApp.Domain.Interfaces.Services;
using FinancasApp.Presentation.Models.Dashboard;
using FinancasApp.Presentation.Models.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FinancasApp.Presentation.Controllers
{
   

    /// <summary>
    /// Controlador para páginas da pasta /Home
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IContaDomainService? _contaDomainService;

        public HomeController(IContaDomainService? contaDomainService)
        {
            _contaDomainService = contaDomainService;
        }

        /// <summary>
        /// Método para abrir a página /Home/Index
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult PesquisarContas(DateTime dataMin, DateTime dataMax)
        {
            try
            {
                //capturar o usuário autenticado no projeto
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(User.Identity.Name);

                //consultar as contas do usuário no domínio
                var contas = _contaDomainService?.Consultar(dataMin, dataMax, usuario.Id);

                #region Calcular o total de contas a receber X pagar

                var graficoDonut = new List<ChartViewModel>();
                graficoDonut.Add(new ChartViewModel
                {
                    Name = "Total de contas a receber",
                    Data = contas.Where(c => c.Tipo == TipoConta.Receber).Sum(c => c.Valor)
                });
                graficoDonut.Add(new ChartViewModel
                {
                    Name = "Total de contas a pagar",
                    Data = contas.Where(c => c.Tipo == TipoConta.Pagar).Sum(c => c.Valor)
                });

                #endregion

                #region Calcular o total de contas por categoria

                var graficoColunas = contas
                    .GroupBy(c => c.Categoria.Nome)
                    .Select(item => new ChartViewModel
                    {
                        Name = item.Key,
                        Data = item.Sum(c => c.Valor)
                    })
                    .ToList();

                #endregion


                return Json(new { graficoDonut, graficoColunas });
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

        }
    }
}



