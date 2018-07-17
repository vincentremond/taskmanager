using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class JsonProjectRepository : IProjectRepository
    {
        private readonly JsonFileRepository<Project> _fileRepository;

        public JsonProjectRepository(IHostingEnvironment hostingEnvironment)
        {
            _fileRepository = new JsonFileRepository<Project>(hostingEnvironment, "projects");
        }

        public Project Get(string projectId)
        {
            using (var projects = _fileRepository.GetDataAsReadOnly())
            {
                return projects.Data.SingleOrDefault(t => t.ProjectId == projectId);
            }
        }

        public void Delete(string projectId)
        {
            using (var projects = _fileRepository.GetDataAsReadWrite())
            {
                projects.Delete(t => t.ProjectId == projectId);
            }
        }

        public IEnumerable<Project> GetAll()
        {
            using (var projects = _fileRepository.GetDataAsReadOnly())
            {
                return projects.Data.ToList();
            }
        }

        public void Upsert(Project project)
        {
            using (var projects = _fileRepository.GetDataAsReadWrite())
            {
                projects.Set(t => t.ProjectId == project.ProjectId, project);
            }
        }
    }
}