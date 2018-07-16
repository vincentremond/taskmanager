using TaskManager.Contract.ViewModel.Model.Context;

namespace TaskManager.Contract.ViewModel.Builder
{
    public interface IContextViewModelBuilder
    {
        Index Index();
        void Create(string title);
        Edit Edit(string contextId);
        void Update(Edit model);
        void Delete(string contextId);
    }
}