using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Business
{
    public interface ITodoBusiness
    {
        IEnumerable<Todo> GetAllActives();
        void IncrementScore(string todoId, int increment);
        void Complete(string todoId);
        void Create(string title);
    }
}
