using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Account
{
    public class RegisterViewModel
    {
        [MaxLength(100, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1}caracteres.")]
        [Required(ErrorMessage = "Por favor, informe seu nome.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe seu email.")]
        public string? Email { get; set; }

        
        [Required(ErrorMessage = "Por favor, informe sua senha.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+*=!])(?!.*\s).{8,}$",
                 ErrorMessage = "Por favor, informe uma senha com letras maiúsculas, minúsculas, símbolos, números e pelo menos 8 caracteres.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha.")]
        public string? SenhaConfirmacao { get; set; }
    }
}


