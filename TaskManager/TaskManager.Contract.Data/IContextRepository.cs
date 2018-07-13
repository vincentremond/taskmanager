using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Data
{
    public interface IContextRepository
    {
        IEnumerable<Context> GetAll();
        void Upsert(Context context);
        Context Get(string contextId);
        void Delete(string contextId);
    }
}
