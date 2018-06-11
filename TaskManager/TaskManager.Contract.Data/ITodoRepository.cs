using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Data
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAllActives();
        void ComputeMetaScore(Todo todo);
        Todo Get(string todoId);
        void Upsert(Todo todo);
    }
}
