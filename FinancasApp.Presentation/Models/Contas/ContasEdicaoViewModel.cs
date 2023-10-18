using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Contas
{
    /// <summary>
    /// Modelo de dados para a página de edição de contas
    /// </summary>
    public class ContasEdicaoViewModel
    {
        public Guid? Id { get; set; } //campo oculto

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, preencha o nome da conta.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, preencha a data da conta.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o valor da conta.")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Por favor, selecione uma categoria.")]
        public Guid? CategoriaId { get; set; }

        [Required(ErrorMessage = "Por favor, marque se o tipo da conta.")]
        public int? Tipo { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(250, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição da conta.")]
        public string? Descricao { get; set; }

        #region Carregamento de dados em campos de seleção

        public List<SelectListItem>? Categorias { get; set; }

        #endregion
    }
}



