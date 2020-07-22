using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using single_endpoint_cqrs.Commands;
using single_endpoint_cqrs.Events;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace single_endpoint_cqrs.Handlers
{
    public class TodoDeleteHandler : IRequestHandler<TodoDeleteCommand, IEnumerable<IEvent>>
    {
        private TodoRepository repo;

        public TodoDeleteHandler(TodoRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<IEvent>> Handle(TodoDeleteCommand cmd, CancellationToken cancellationToken)
        {
            var todo = repo.GetById(cmd.Id);
            if (todo == null) throw new System.Exception($"No todo with id ${cmd.Id} exists");
            repo.Delete(todo);
            return new List<IEvent> { new TodoDeleted() { Id = todo.Id }};
        }
    }
}
