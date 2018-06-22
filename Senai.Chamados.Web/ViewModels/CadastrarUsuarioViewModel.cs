using System.ComponentModel.DataAnnotations;

namespace Senai.Chamados.Web.ViewModels
{
    public class CadastrarUsuarioViewModel
    {
        [Display(Name = "Informe o nome")]
        [Required(ErrorMessage = "Informe o campo nome")]
        public string Nome { get; set; }

        [Display(Name = "Informe o Email")]
        [Required(ErrorMessage = "Informe o campo email")]
        [EmailAddress(ErrorMessage = "O Email é inválido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Informe o Telefone")]
        [Required(ErrorMessage = "Informe o campo telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Display(Name = "Informe a senha")]
        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}