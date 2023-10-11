using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Accounts;
public class LoginViewModel
{
    [Required(ErrorMessage = "Informe um E-mail!")]
    [EmailAddress(ErrorMessage = "E-mail inválido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe a senha.")]
    public string Password { get; set; }
}
