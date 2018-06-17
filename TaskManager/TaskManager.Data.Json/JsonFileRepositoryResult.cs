using System;
using System.Collections.Generic;
using System.Threading;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    internal class JsonFileRepositoryResult<T> : IDisposable
    {
        private JsonFileRepository<T> _jsonFileRepository;
        private readonly bool _updateOnDispose;
        private readonly SemaphoreSlim _semaphore;

        public JsonFileRepositoryResult(JsonFileRepository<T> jsonFileRepository, bool updateOnDispose, SemaphoreSlim semaphore)
        {
            _jsonFileRepository = jsonFileRepository;
            _updateOnDispose = updateOnDispose;
            _semaphore = semaphore;
        }

        public List<T> Data { get; internal set; }

        public void Dispose()
        {
            if(_updateOnDispose)
            {
                _jsonFileRepository.UpdateDada(Data);
            }
            _semaphore.Release();
        }

        internal void Set(Predicate<T> predicate, T todo)
        {
            var index = Data.FindIndex(predicate);
            if (index == -1)
            {
                Data.Add(todo);
            }
            else
            {
                Data[index] = todo;
            }
        }
    }
}