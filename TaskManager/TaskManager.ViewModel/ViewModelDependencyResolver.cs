using Microsoft.Extensions.DependencyInjection;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.ViewModel.Builder;

namespace TaskManager.ViewModel
{
    public static class ViewModelDependencyResolver
    {
        public static void AddViewModel(this IServiceCollection services)
        {
            services.AddTransient<ITodoViewModelBuilder, TodoViewModelBuilder>();
        }
    }
}
