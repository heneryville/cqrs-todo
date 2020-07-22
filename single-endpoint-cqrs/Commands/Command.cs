using MediatR;
using single_endpoint_cqrs.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace single_endpoint_cqrs.Commands
{
    [JsonConverter(typeof(CommandConverter))]
    public abstract class Command : IRequest<IEnumerable<IEvent>>
    {
        public string Type { get; set; }
        public string Id { get; set; }
    }

}
