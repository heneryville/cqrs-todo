using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace single_endpoint_cqrs
{
    public class TodoRepository
    {
        private List<Todo> todos = new List<Todo> {
            new Todo {  Id = Guid.NewGuid().ToString(), IsCompleted = false, Title = "My first To-Do"}
        };

        public void Create(Todo todo)
        {
            todos.Add(todo);
        }
        public void Update(Todo todo)
        {
            this.Delete(todo);
            this.Create(todo);
        }
        public void Delete(Todo todo)
        {
            todos.Remove(GetById(todo.Id));
        }

        internal IEnumerable<Todo> Read()
        {
            return this.todos;
        }

        internal Todo GetById(string id)
        {
            return todos.Find(x => x.Id == id);
        }
    }

    public class Todo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
