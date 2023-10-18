using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Contas
{
    public class ContasCadastroViewModel
    {
        [MinLength(3, ErrorMessage = "Informe no minimo {1} caracteres")]
        [MaxLength(15, ErrorMessage = "Informe no minimo {1} caracteres")]
        [Required(ErrorMessage = "Preencha o nome da conta.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Preencha a data da conta.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Preencha o valor da conta.")]
        public Decimal? Valor { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria.")]
        public Guid? CategoriaId { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de conta.")]
        public int? Tipo { get; set; }


        public string? Descricao { get; set; }

        #region Carregamento de dados em campos de seleção

        public List<SelectListItem>? Categorias { get; set; }

        #endregion

    }
}
