using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Data
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAllActives();
        Todo Get(string todoId);
        void Upsert(Todo todo);
    }
}
