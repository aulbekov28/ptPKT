using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities
{
    public class ToDoItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }
        public bool IsDone { get; private set; }

        public void MarkComplete()
        {
            IsDone = true;
            // Events.Add(new ToDoItemCompletedEvent(this));
        }
    }
}