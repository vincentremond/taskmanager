using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Context;
using TaskManager.Models;

namespace TaskManager.ViewModel.Builder
{
    public class ContextViewModelBuilder : IContextViewModelBuilder
    {
        private readonly IContextBusiness _contextBusiness;

        public ContextViewModelBuilder(IContextBusiness contextBusiness)
        {
            _contextBusiness = contextBusiness;
        }

        public Index Index()
        {
            var todos = _contextBusiness.GetAll();
            var result = new Index
            {
                Items = todos.Select(t => new Index.Item()
                {
                    ContextId = t.ContextId,
                    Title = t.Title,
                }).ToList()
            };
            return result;
        }

        public Edit Edit(string id)
        {
            var context = _contextBusiness.Get(id);
            var result = new Edit
            {
                ContextId = context.ContextId,
                Title = context.Title,
            };
            return result;
        }

        public void Update(Edit model)
        {
            var todo = _contextBusiness.Get(model.ContextId);
            todo.Title = model.Title;
            _contextBusiness.SaveChanges(todo);
        }

        public void Delete(string contextId)
        {
            _contextBusiness.Delete(contextId);
        }

        public IEnumerable<Context> GetAll()
        {
            return _contextBusiness.GetAll();
        }

        public void Create(string title)
        {
            _contextBusiness.Create(title);
        }
    }
}
