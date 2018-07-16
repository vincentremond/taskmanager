using System.Collections.Generic;
using TaskManager.Contract.ViewModel.Model.Context;
using TaskManager.Models;

namespace TaskManager.Contract.ViewModel.Builder
{
    public interface IContextViewModelBuilder
    {
        Index Index();
        void Create(string title);
        Edit Edit(string contextId);
        void Update(Edit model);
        void Delete(string contextId);
        IEnumerable<Context> GetAll();
    }
}