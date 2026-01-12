using System.ComponentModel.DataAnnotations;

public class User
{
    [Display(Name = "Lembrar-me?")]
    public bool RememberMe { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress]
    public string Email { get; set; }
}