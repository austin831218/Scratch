using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using OAuth2.API.Services;
using Telerik.OpenAccess;

namespace OAuth2.API
{
    public class AutoFacConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UserService>();

            builder.Register<EntitiesContext>(x =>
            {
                return new EntitiesContext("AuthConnection");
            }).As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IAsyncRepository<>));

            var container = builder.Build();


            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            //DependencyResolver.SetResolver(resolver);

            return container;
        }
    }
}