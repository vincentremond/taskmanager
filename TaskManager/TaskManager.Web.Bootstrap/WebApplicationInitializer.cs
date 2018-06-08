using Microsoft.Extensions.DependencyInjection;
using TaskManager.Business;
using TaskManager.ViewModel;

namespace TaskManager.Web.Bootstrap
{
    public static class WebApplicationInitializer
    {
        public static void AddTaskManagerWebApp(this IServiceCollection services)
        {
            services.AddBusiness();
            services.AddViewModel();
        }
    }
}
