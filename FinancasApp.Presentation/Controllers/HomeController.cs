using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancasApp.Presentation.Controllers
{
    /// <summary>
    /// Controlador para páginas da pasta /Home
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Método para abrir a página /Home/Index
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}



