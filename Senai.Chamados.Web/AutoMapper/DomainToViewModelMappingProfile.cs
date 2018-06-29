using AutoMapper;
using Senai.Chamados.Domain.Entidades;
using Senai.Chamados.Web.ViewModels;
using Senai.Chamados.Web.ViewModels.Usuario;

namespace Senai.Chamados.Web.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap(typeof(UsuarioDomain), typeof(CadastrarUsuarioViewModel));
            Mapper.CreateMap(typeof(UsuarioDomain), typeof(UsuarioViewModel));
        }
    }
}