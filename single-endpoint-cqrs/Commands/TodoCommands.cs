using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace single_endpoint_cqrs.Commands
{
    public class TodoCreateCommand : Command
    {
        public string Title { get; set; }
    }

    public class TodoToggleCompleteCommand : Command
    {
        public bool PriorCompleteState { get; set; }
    }
    public class TodoDeleteCommand : Command
    { }
}
