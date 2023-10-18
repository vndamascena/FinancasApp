using FinancasApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Contas
{
    /// <summary>
    /// Modelo de dados para a página de consulta de contas
    /// </summary>
    public class ContasConsultaViewModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public DateTime? DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public DateTime? DataMax { get; set; }

        /// <summary>
        /// Lista de contas que será exibida na página
        /// </summary>
        public List<Conta>? Contas { get; set; }
    }
}



