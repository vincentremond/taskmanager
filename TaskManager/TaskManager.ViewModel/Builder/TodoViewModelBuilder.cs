﻿using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Todo;

namespace TaskManager.ViewModel.Builder
{
    public class TodoViewModelBuilder : ITodoViewModelBuilder
    {
        private readonly ITodoBusiness _todoBusiness;

        public TodoViewModelBuilder(ITodoBusiness todoBusiness)
        {
            _todoBusiness = todoBusiness;
        }

        public Index Index()
        {
            var todos = _todoBusiness.GetAllActives();
            var result = new Index();
            result.Items = todos.Select(t => new Index.Item()
            {
                TodoId = t.TodoId,
                Title = t.Title,
                MetaScore =  t.MetaScore,
            }).ToList();
            return result;
        }
    }
}
