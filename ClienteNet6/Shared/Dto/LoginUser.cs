using System.ComponentModel.DataAnnotations;

namespace ClienteNet6.Shared.Dto
{
    public class LoginUser
    {
        [Required(ErrorMessage ="Usuário obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Senha obrigatória.")]
        [StringLength(24, MinimumLength = 8, ErrorMessage = "Senha deve conter entre 8 e 24 caracteres.")]
        public string Password { get; set; }
    }
}
