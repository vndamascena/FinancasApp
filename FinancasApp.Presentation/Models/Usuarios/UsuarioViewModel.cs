namespace FinancasApp.Presentation.Models.Usuarios
{
    /// <summary>
    /// Modelo de dados para armazenar as informações
    /// do usuário autenticado
    /// </summary>
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime DataHoraAcesso { get; set; }
    }
}



