using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Data
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        void Upsert(Project project);
        Project Get(string projectId);
        void Delete(string projectId);
    }
}
