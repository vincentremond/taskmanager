using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace TaskManager.Data.Json
{
    internal class JsonFileRepository<T>
    {
        private string _file;
        private SemaphoreSlim _semaphore;

        public JsonFileRepository(IHostingEnvironment hostingEnvironment, string name)
        {
            _file = Path.Combine(hostingEnvironment.ContentRootPath, "Data", $"{name}.json");
            _semaphore = new SemaphoreSlim(1);
        }

        internal JsonFileRepositoryResult<T> GetDataAsReadOnly() => GetData(false);
        internal JsonFileRepositoryResult<T> GetDataAsReadWrite() => GetData(true);
        protected JsonFileRepositoryResult<T> GetData(bool updateOnDispose)
        {
            _semaphore.Wait(); // WaitAsync
            var result = new JsonFileRepositoryResult<T>(this, updateOnDispose, _semaphore);
            if (!File.Exists(_file))
            {
                result.Data = new List<T>();
            }
            else
            {
                var contents = File.ReadAllText(_file);
                result.Data = JsonConvert.DeserializeObject<List<T>>(contents);
            }
            return result;
        }

        internal void UpdateDada(List<T> data)
        {
            var contents = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_file, contents);
        }
    }
}
