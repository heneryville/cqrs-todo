using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace single_endpoint_cqrs.Events
{
    public abstract class TodoEvent : IEvent
    {
        public string Topic { get { return "Todo"; } }
        public string Id { get; set; }
    }
    public class TodoCreated : TodoEvent
    {
        public string Title { get; set; }
    }
    public class TodoUpdated : TodoEvent
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
    public class TodoDeleted : TodoEvent { }
}
