using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models.Account
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o seu email de acesso.")]
        public string Email { get; set; }


        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+*=!])(?!.*\s).{8,}$",
            ErrorMessage = "Por favor, informe uma senha com letras maiúsculas, minúsculas, símbolos, números e pelo menos 8 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a sua senha de acesso.")]
        public string Senha { get; set; }
    }
}
