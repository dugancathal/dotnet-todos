using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static readonly ConcurrentDictionary<string, TodoItem> _todos =
            new ConcurrentDictionary<string, TodoItem>();

        public TodoRepository()
        {
            var key = Guid.NewGuid().ToString();
            _todos[key] = new TodoItem(key, "My first todo");
        }

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _todos[item.Key] = item;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todos.Values;
        }

        public TodoItem Find(string key)
        {
            return _todos[key];
        }

        public TodoItem Remove(string key)
        {
            TodoItem item;
            _todos.TryRemove(key, out item);
            return item;
        }

        public void Update(TodoItem item)
        {
            _todos[item.Key] = item;
        }
    }
}
