using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http.Filters;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Ninject.Web.WebApi.FilterBindingSyntax;
using refactor_me;
using refactor_me.Attributes;
using refactor_me.Filters;
using Refactor.Model.Persistance;
using Refactor.Persistance;
using Refactor.Persistance.Repositories;
using Refactor.Web.Common;
using Refactor.Web.Common.Mappings;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace refactor_me
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<ModelPersistanceContext>().InRequestScope().
                WithConstructorArgument(typeof(string), "name=ModelContext");
            kernel.Bind(typeof(IRepository<>)).To(typeof(EntityFrameworkRepository<>)).InRequestScope();
            kernel.Bind<IEntityMapper>().To<EntityMapper>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<NonTransactionalUnitOfWork>();

            kernel.BindHttpFilter<UnitOfWorkActionFilter>(FilterScope.Controller).WhenControllerHas<UnitOfWorkAttribute>();
            kernel.BindHttpFilter<UnitOfWorkActionFilter>(FilterScope.Action).WhenActionMethodHas<UnitOfWorkAttribute>();
        }
    }
}