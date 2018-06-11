using Microsoft.Extensions.DependencyInjection;
using TaskManager.Business;
using TaskManager.Contract.Business;
using TaskManager.Contract.Data;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Data.Json;
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
        }

        static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<ITodoBusiness, TodoBusiness>();
        }

        static void AddViewModel(this IServiceCollection services)
        {
            services.AddTransient<ITodoViewModelBuilder, TodoViewModelBuilder>();
        }

        static void AddDataJson(this IServiceCollection services)
        {
            services.AddTransient<ITodoRepository, JsonTodoRepository>();
        }
    }
}
