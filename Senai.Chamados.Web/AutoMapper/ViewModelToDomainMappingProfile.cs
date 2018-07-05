using AutoMapper;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.ViewModels;
using Senai.Chamados.Web.ViewModels.Chamado;
using Senai.Chamados.Web.ViewModels.Usuario;

namespace Senai.Chamados.Web.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap(typeof(CadastrarUsuarioViewModel), typeof(UsuarioDomain));
            Mapper.CreateMap(typeof(UsuarioViewModel), typeof(UsuarioDomain));

            Mapper.CreateMap(typeof(ChamadoViewModel), typeof(ChamadoDomain));
        }
    }
}