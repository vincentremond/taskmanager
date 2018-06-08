using Microsoft.Extensions.DependencyInjection;
using TaskManager.Contract.Business;

namespace TaskManager.Business
{
    public static class BusinessDependencyResolver
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<ITodoBusiness, TodoBusiness>();
        }
    }
}
