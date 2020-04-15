using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using BDCO.Repository;
using BDCO.Web.Controllers;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BDCO.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
      //  string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<HomeController>().InstancePerRequest();

            //builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();
            ////builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
            //builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();
            //builder.RegisterType(typeof(IEventHandler<IEvent>)).As(typeof(IEventHandler<IEvent>));
            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
                        

            var services = Assembly.Load("BDCO.Domain");
            //builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(ICommandHandler<>));
            //builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IEventHandler<>));
            //builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IQueryHandler<,>));
            builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IRepository<>));
            //builder.RegisterAssemblyTypes(services).AsClosedTypesOf(typeof(IValidationHandler<>));
           // SqlDependency.Start(connString);
            //builder.RegisterGenericDecorator(typeof(CommandValidationHandler<>), typeof(ICommandHandler<>), fromKey: "handler");

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            // GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        //protected void Application_End()
        //{
        //    //Stop SQL dependency
        //    SqlDependency.Stop(connString);
        //}
    }
}
