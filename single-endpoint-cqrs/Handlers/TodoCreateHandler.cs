using MediatR;
using single_endpoint_cqrs.Commands;
using single_endpoint_cqrs.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace single_endpoint_cqrs.Handlers
{
    public class TodoCreateHandler : IRequestHandler<TodoCreateCommand, IEnumerable<IEvent>>
    {
        private TodoRepository repo;

        public TodoCreateHandler(TodoRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<IEvent>> Handle(TodoCreateCommand cmd, CancellationToken cancellationToken)
        {
            var todo = repo.GetById(cmd.Id);
            if (todo != null) throw new System.Exception($"Todo with id ${cmd.Id} already exists");
            repo.Create(new Todo()
            {
                Id = cmd.Id,
                IsCompleted = false,
                Title = cmd.Title
            });

            return new List<IEvent> { new TodoCreated() { Id = cmd.Id, Title = cmd.Title } };
        }
    }
}
