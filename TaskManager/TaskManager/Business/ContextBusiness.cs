using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Business;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Business
{
    public class ContextBusiness : IContextBusiness
    {
        private readonly IContextRepository _contextRepository;

        public ContextBusiness(IContextRepository contextRepository)
        {
            _contextRepository = contextRepository;
        }

        public IEnumerable<Context> GetAll()
        {
            return _contextRepository
                .GetAll()
                .OrderBy(t => t.Title)
                .ToList();
        }

        public void Delete(string contextId)
        {
            _contextRepository.Delete(contextId);
        }

        public Context Get(string contextId)
        {
            return _contextRepository.Get(contextId);
        }

        public void SaveChanges(Context context)
        {
            context.DateModified = DateTimeOffset.Now;
            _contextRepository.Upsert(context);
        }
    }
}
