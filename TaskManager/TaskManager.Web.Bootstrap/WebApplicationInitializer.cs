using Microsoft.Extensions.DependencyInjection;
using TaskManager.Business;
using TaskManager.Contract.Business;
using TaskManager.Contract.Data;
using TaskManager.Contract.Utilities;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Data.Json;
using TaskManager.Utilities;
using TaskManager.ViewModel.Builder;

namespace TaskManager.Web.Bootstrap
{
    public static class WebApplicationInitializer
    {
        public static void AddTaskManagerWebApp(this IServiceCollection services)
        {
            services.AddBusiness();
            services.AddViewModel();
            services.AddDataJson();
            services.AddUtilities();
        }

        static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IContextBusiness, ContextBusiness>();
            services.AddTransient<IProjectBusiness, ProjectBusiness>();
            services.AddTransient<ITodoBusiness, TodoBusiness>();
            services.AddTransient<ITodoEnricher, TodoEnricher>();
        }

        static void AddViewModel(this IServiceCollection services)
        {
            services.AddTransient<ITodoViewModelBuilder, TodoViewModelBuilder>();
            services.AddTransient<IContextViewModelBuilder, ContextViewModelBuilder>();
            services.AddTransient<IProjectViewModelBuilder, ProjectViewModelBuilder>();
        }

        static void AddDataJson(this IServiceCollection services)
        {
            services.AddTransient<ITodoRepository, JsonTodoRepository>();
            services.AddTransient<IContextRepository, JsonContextRepository>();
            services.AddTransient<IProjectRepository, JsonProjectRepository>();
        }
        static void AddUtilities(this IServiceCollection services)
        {
            services.AddTransient<IIdentifierProvider, IdentifierProvider>();
            services.AddTransient<ICloneProvider, CloneProvider>();
        }
    }
}
