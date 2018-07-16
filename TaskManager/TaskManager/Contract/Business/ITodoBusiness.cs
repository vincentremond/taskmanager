using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Business
{
    public interface ITodoBusiness
    {
        IEnumerable<MetaTodo> GetAllActives();
        void IncrementScore(string todoId, int increment);
        void Complete(string todoId);
        void Create(string title);
        MetaTodo Get(string todoId);
        void SaveChanges(Todo todo);
    }
}
