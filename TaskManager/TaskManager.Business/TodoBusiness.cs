using System.Collections.Generic;
using TaskManager.Contract.Business;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class TodoBusiness : ITodoBusiness
    {
        private readonly ITodoRepository _repository;

        public TodoBusiness(ITodoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Todo> GetAllActives()
        {
            return _repository.GetAllActives();
        }
    }
}
