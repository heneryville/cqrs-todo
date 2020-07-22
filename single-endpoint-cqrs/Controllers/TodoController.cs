using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace single_endpoint_cqrs.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly TodoRepository repo;

        public TodoController(ILogger<TodoController> logger, TodoRepository repo)
        {
            _logger = logger;
            this.repo = repo;
        }

        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            return this.repo.Read();
        }
    }
}
