using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using single_endpoint_cqrs.Commands;
using single_endpoint_cqrs.Events;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace single_endpoint_cqrs.Handlers
{
    public class TodoToggleCompleteHandler : IRequestHandler<TodoToggleCompleteCommand, IEnumerable<IEvent>>
    {
        private TodoRepository repo;

        public TodoToggleCompleteHandler(TodoRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<IEvent>> Handle(TodoToggleCompleteCommand cmd, CancellationToken cancellationToken)
        {
            var todo = repo.GetById(cmd.Id);
            if (todo == null) throw new System.Exception($"No todo with id ${cmd.Id} exists");
            if (todo.IsCompleted != cmd.PriorCompleteState) throw new System.Exception($"Expected prior state of ${cmd.Id} to be ${todo.IsCompleted} ");
            todo.IsCompleted = !cmd.PriorCompleteState;
            repo.Update(todo);
            return new List<IEvent> { new TodoUpdated() {
                Title = todo.Title,
                IsCompleted = todo.IsCompleted,
                Id = todo.Id 
            }};
        }
    }
}
