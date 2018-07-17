using System.Collections.Generic;
using TaskManager.Contract.ViewModel.Model.Project;
using TaskManager.Models;

namespace TaskManager.Contract.ViewModel.Builder
{
    public interface IProjectViewModelBuilder
    {
        Index Index();
        void Create(string title);
        Edit Edit(string contextId);
        void Update(Edit model);
        void Delete(string contextId);
        IEnumerable<Project> GetAll();
    }
}