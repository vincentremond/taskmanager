using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Business
{
    public interface IContextBusiness
    {
        IEnumerable<Context> GetAll();
        Context Get(string contextId);
        void SaveChanges(Context todo);
        void Create(string title);
        void Delete(string contextId);
    }
}
