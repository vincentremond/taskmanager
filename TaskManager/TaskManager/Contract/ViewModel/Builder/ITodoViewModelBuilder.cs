using TaskManager.Contract.ViewModel.Model.Todo;

namespace TaskManager.Contract.ViewModel.Builder
{
    public interface ITodoViewModelBuilder
    {
        Index Index();
        Edit Edit(string todoId);
        EditViewModel EditViewModel(Edit edit);
        void Update(Edit model);
        void Complete(string todoId);
        void IncrementScore(string todoId, int increment);
        void Create(string title);
    }
}
