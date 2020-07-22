using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace single_endpoint_cqrs.Events
{
    public interface IEvent
    {
        public string Topic { get; }
        public string Id { get; set; }
    }
}
