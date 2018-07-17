using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Project;
using TaskManager.Models;

namespace TaskManager.ViewModel.Builder
{
    public class ProjectViewModelBuilder : IProjectViewModelBuilder
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectViewModelBuilder(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        public Index Index()
        {
            var todos = _projectBusiness.GetAll();
            var result = new Index
            {
                Items = todos.Select(t => new Index.Item()
                {
                    ProjectId = t.ProjectId,
                    Title = t.Title,
                }).ToList()
            };
            return result;
        }

        public Edit Edit(string id)
        {
            var project = _projectBusiness.Get(id);
            var result = new Edit
            {
                ProjectId = project.ProjectId,
                Title = project.Title,
            };
            return result;
        }

        public void Update(Edit model)
        {
            var todo = _projectBusiness.Get(model.ProjectId);
            todo.Title = model.Title;
            _projectBusiness.SaveChanges(todo);
        }

        public void Delete(string projectId)
        {
            _projectBusiness.Delete(projectId);
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectBusiness.GetAll();
        }

        public void Create(string title)
        {
            _projectBusiness.Create(title);
        }
    }
}
