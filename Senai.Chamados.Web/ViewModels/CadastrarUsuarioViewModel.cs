using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Senai.Chamados.Web.ViewModels
{
    public class CadastrarUsuarioViewModel : BaseViewModel
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

        [Required(ErrorMessage ="Informe o cpf")]
        [MaxLength(14)]
        public string Cpf { get; set; }

        [Display(Name = "Informe a senha")]
        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [MaxLength(8, ErrorMessage ="Número máximo de caracteres é 8")]
        [MinLength(4,ErrorMessage ="Número mínimo de caracteres é 4")]
        public string Senha { get; set; }
                
        public SelectList Sexo { get; set; }
        [Required(ErrorMessage = "Informe o sexo")]
        public string SexoId { get; set; }

        [MaxLength(9,ErrorMessage = "Cep deve possui no máximo 9 caracteres")]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}