﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Contract.Data;
using TaskManager.Models;

namespace TaskManager.Data.Json
{
    public class JsonTodoRepository : ITodoRepository
    {
        private static readonly ConcurrentBag<Todo> data;

        static JsonTodoRepository()
        {

            data = new ConcurrentBag<Todo>();

            void AddItem(string todoId, string title, bool completed, int score)
            {
                data.Add(new Todo
                {
                    TodoId = new Guid(todoId),
                    Title = title,
                    Completed = completed,
                    Score = score,
                });
            };

            AddItem("dda44d8e-cbb7-4b07-bcd6-9ad1599e6a8f", "Task 1", false, 10);
            AddItem("79075496-f921-4856-8bb2-7669cbc7d7c4", "Task 2", false, 12);
            AddItem("243af397-79d5-4d70-b864-2bdef9b4b7eb", "Task 0", true, 13);
        }

        public IEnumerable<Todo> GetAllActives()
        {
            var todos = data.Where(t => !t.Completed).ToList();
            todos.ForEach(ComputeMetaScore);
            return todos.OrderByDescending(t => t.MetaScore).ToList();
        }

        public void ComputeMetaScore(Todo todo)
        {
            todo.MetaScore = todo.Score;
        }
    }
}