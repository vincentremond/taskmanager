using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Business
{
    public interface IProjectBusiness
    {
        IEnumerable<Project> GetAll();
        Project Get(string projectId);
        void SaveChanges(Project todo);
        void Delete(string projectId);
    }
}
