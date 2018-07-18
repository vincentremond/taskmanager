using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Contract.Business;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Todo;

namespace TaskManager.ViewModel.Builder
{
    public class TodoViewModelBuilder : ITodoViewModelBuilder
    {
        private readonly ITodoBusiness _todoBusiness;
        private readonly IContextBusiness _contextBusiness;
        private readonly IProjectBusiness _projectBusiness;
        private readonly IMapper _mapper;

        public TodoViewModelBuilder(ITodoBusiness todoBusiness
            , IContextBusiness contextBusiness
            , IProjectBusiness projectBusiness
            , IMapper mapper
            )
        {
            _todoBusiness = todoBusiness;
            _contextBusiness = contextBusiness;
            _projectBusiness = projectBusiness;
            _mapper = mapper;
        }

        public Index Index(string[] context, string[] project)
        {
            var todos = _todoBusiness.GetAllActives();

            var drafts = todos.Where(t => t.IsDraft);

            var toDisplay = todos.Where(t => !t.IsDraft);
            if (context != null && context.Any())
            {
                toDisplay = toDisplay.Where(t => context.Contains(t.Context.ContextId));
            }
            if (project != null && project.Any())
            {
                toDisplay = toDisplay.Where(t => project.Contains(t.Project.ProjectId));
            }


            var result = new Index
            {
                Drafts = new Index.DraftInfos
                {
                    Count = drafts.Count(),
                    FirstTodoId = drafts.FirstOrDefault()?.TodoId,
                },
                Items = toDisplay.Select(t => new Index.Item()
                {
                    TodoId = t.TodoId,
                    Title = t.Title,
                    MetaScore = t.MetaScore,
                    Context = new Index.Context
                    {
                        ContextId = t.Context.ContextId,
                        Title = t.Context.Title,
                        Color = t.Context.Color,
                    },
                    Project = new Index.Project()
                    {
                        ProjectId = t.Project.ProjectId,
                        Title = t.Project.Title,
                        Color = t.Project.Color,
                    },
                    Url = t.Url,
                }).ToList()
            };
            return result;
        }

        public Edit Edit(string id)
        {
            var todo = _todoBusiness.Get(id);
            var result = new Edit
            {
                TodoId = todo.TodoId,
                Title = todo.Title,
                Complexity = todo.Complexity,
                Description = todo.Description,
                ContextId = todo.Context?.ContextId,
                ProjectId = todo.Project?.ProjectId,
                Url = todo.Url,
            };
            return result;
        }

        public EditViewModel EditViewModel(Edit edit)
        {
            var newModel = _mapper.Map<EditViewModel>(edit);
            newModel.ViewData = new EditViewModel.Data
            {
                Contexts = new SelectList(_contextBusiness.GetAll(), "ContextId", "Title", edit.ContextId),
                Projects = new SelectList(_projectBusiness.GetAll(), "ProjectId", "Title", edit.ProjectId),
            };
            return newModel;
        }

        public void Update(Edit model)
        {
            var todo = _todoBusiness.Get(model.TodoId);
            var context = _contextBusiness.Get(model.ContextId);
            var project = _projectBusiness.Get(model.ProjectId);
            todo.Title = model.Title;
            todo.Complexity = model.Complexity;
            todo.Description = model.Description;
            todo.Context = context;
            todo.Project = project;
            todo.Url = model.Url;
            _todoBusiness.SaveChanges(todo);
        }

        public void Complete(string todoId)
        {
            _todoBusiness.Complete(todoId);
        }

        public void IncrementScore(string todoId, int increment)
        {
            _todoBusiness.IncrementScore(todoId, increment);
        }

        public void Create(string title)
        {
            _todoBusiness.Create(title);
        }
    }
}
