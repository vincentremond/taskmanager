using TaskManager.Contract.ViewModel.Model;

namespace TaskManager.Contract.ViewModel.Builder
{
    public interface ITodoViewModelBuilder
    {
        TodoIndexViewModel GetIndex();
    }
}
