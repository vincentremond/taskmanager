using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class JsonContextRepository : IContextRepository
    {
        private readonly JsonFileRepository<Context> _fileRepository;

        public JsonContextRepository(IHostingEnvironment hostingEnvironment)
        {
            _fileRepository = new JsonFileRepository<Context>(hostingEnvironment, "context");
        }

        public Context Get(string contextId)
        {
            using (var contexts = _fileRepository.GetDataAsReadOnly())
            {
                return contexts.Data.SingleOrDefault(t => t.ContextId == contextId);
            }
        }

        public void Delete(string contextId)
        {
            using (var contexts = _fileRepository.GetDataAsReadWrite())
            {
                contexts.Delete(t => t.ContextId == contextId);
            }
        }

        public IEnumerable<Context> GetAll()
        {
            using (var contexts = _fileRepository.GetDataAsReadOnly())
            {
                return contexts.Data.ToList();
            }
        }

        public void Upsert(Context context)
        {
            using (var contexts = _fileRepository.GetDataAsReadWrite())
            {
                contexts.Set(t => t.ContextId == context.ContextId, context);
            }
        }
    }
}