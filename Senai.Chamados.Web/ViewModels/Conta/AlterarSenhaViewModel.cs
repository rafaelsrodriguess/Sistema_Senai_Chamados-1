using System.ComponentModel.DataAnnotations;

namespace Senai.Chamados.Web.ViewModels.Conta
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe a senha Atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        [MinLength(4, ErrorMessage ="A senha deve ter pelo menos 4 caracteres")]
        public string SenhaAtual { get; set; }


        [Required(ErrorMessage = "Informe a nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        [MinLength(4, ErrorMessage = "A senha deve ter pelo menos 4 caracteres")]
        public string NovaSenha { get; set; }


        [Required(ErrorMessage = "Informe a sua nova senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [MinLength(4, ErrorMessage = "A senha deve ter pelo menos 4 caracteres")]
        [Compare(nameof(NovaSenha), ErrorMessage = "A senha e a confirmação de senha não estão iguais")]
        public string ConfirmarSenha { get; set; }

    }
}