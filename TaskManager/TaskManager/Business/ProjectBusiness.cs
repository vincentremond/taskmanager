using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectBusiness(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<Project> GetAll()
        {
            return _projectRepository
                .GetAll()
                .OrderBy(t => t.Title)
                .ToList();
        }

        public void Create(string title)
        {
            var project = new Project
            {
                ProjectId = Guid.NewGuid().ToString("N"),
                Title = title,
                DateCreated = DateTimeOffset.Now,
                DateModified = DateTimeOffset.Now,
            };
            SaveChanges(project);
        }

        public void Delete(string projectId)
        {
            _projectRepository.Delete(projectId);
        }

        public Project Get(string projectId)
        {
            return _projectRepository.Get(projectId);
        }

        public void SaveChanges(Project project)
        {
            project.DateModified = DateTimeOffset.Now;
            _projectRepository.Upsert(project);
        }
    }
}
