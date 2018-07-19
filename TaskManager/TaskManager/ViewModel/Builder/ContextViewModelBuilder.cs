using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.Utilities;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Context;
using TaskManager.Models;

namespace TaskManager.ViewModel.Builder
{
    public class ContextViewModelBuilder : IContextViewModelBuilder
    {
        private readonly IContextBusiness _contextBusiness;
        private readonly IIdentifierProvider _identifierProvider;

        public ContextViewModelBuilder(IContextBusiness contextBusiness
            , IIdentifierProvider identifierProvider
            )
        {
            _contextBusiness = contextBusiness;
            _identifierProvider = identifierProvider;
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
                    Color = t.Color,
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
                Color = context.Color,
            };
            return result;
        }

        public void Update(Edit model)
        {
            var context = _contextBusiness.Get(model.ContextId);
            context.Title = model.Title;
            context.Color = model.Color;
            _contextBusiness.SaveChanges(context);
        }

        public void Delete(string contextId)
        {
            _contextBusiness.Delete(contextId);
        }

        public IEnumerable<Context> GetAll()
        {
            return _contextBusiness.GetAll();
        }

        public void Create(Add model)
        {
            var context = new Context
            {
                ContextId = _identifierProvider.CreateNew(),
                Title = model.Title,
                Color = model.Color,
                DateCreated = DateTimeOffset.Now,
                DateModified = DateTimeOffset.Now,
            };
            _contextBusiness.SaveChanges(context);
        }
    }
}
