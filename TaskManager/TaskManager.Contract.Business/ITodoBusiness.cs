using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.Business
{
    public interface ITodoBusiness
    {
        IEnumerable<Todo> GetAllActives();
    }
}
