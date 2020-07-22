using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using single_endpoint_cqrs.Commands;
using single_endpoint_cqrs.Events;

namespace single_endpoint_cqrs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ILogger<CommandsController> _logger;

        public CommandsController(ILogger<CommandsController> logger, IMediator commandBus)
        {
            _logger = logger;
            CommandBus = commandBus;
        }

        public IMediator CommandBus { get; }

        [HttpPost]
        public async Task<IEnumerable<IEvent>> Post(Command cmd)
        {
            // A real implementation would then store the commands into an eventstore
            return await CommandBus.Send(cmd);
        }
    }
}
