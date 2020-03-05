using ptPKT.Core.Entities;
using ptPKT.Core.Entities.BL;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Events
{
    public class ToDoItemCompletedEvent : BaseDomainEvent
    {
        public ToDoItem CompletedItem { get; set; }

        public ToDoItemCompletedEvent(ToDoItem completedItem)
        {
            CompletedItem = completedItem;
        }
    }
}
