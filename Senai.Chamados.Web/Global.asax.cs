using Senai.Chamados.Web.AutoMapper;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Senai.Chamados.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutoMapperConfig.RegisterMapping();
        }
    }
}
