using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.Utilities;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Project;
using TaskManager.Models;

namespace TaskManager.ViewModel.Builder
{
    public class ProjectViewModelBuilder : IProjectViewModelBuilder
    {
        private readonly IProjectBusiness _projectBusiness;
        private readonly IIdentifierProvider _identifierProvider;

        public ProjectViewModelBuilder(IProjectBusiness projectBusiness
            , IIdentifierProvider identifierProvider
            )
        {
            _projectBusiness = projectBusiness;
            _identifierProvider = identifierProvider;
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
                    Color = t.Color,
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
                Color = project.Color,
            };
            return result;
        }

        public void Update(Edit model)
        {
            var project = _projectBusiness.Get(model.ProjectId);
            project.Title = model.Title;
            project.Color = model.Color;
            _projectBusiness.SaveChanges(project);
        }

        public void Delete(string projectId)
        {
            _projectBusiness.Delete(projectId);
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectBusiness.GetAll();
        }

        public void Create(Add model)
        {
            var project = new Project
            {
                ProjectId = _identifierProvider.CreateNew(),
                Title = model.Title,
                Color = model.Color,
                DateCreated = DateTimeOffset.Now,
                DateModified = DateTimeOffset.Now,
            };
            _projectBusiness.SaveChanges(project);
        }
    }
}
