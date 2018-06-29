using AutoMapper;

namespace Senai.Chamados.Web.AutoMapper
{
    public class AutoMapperConfig
    {

        public static void RegisterMapping()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}