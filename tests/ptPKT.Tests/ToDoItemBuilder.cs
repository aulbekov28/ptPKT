using ptPKT.Core.Entities;

namespace ptPKT.Tests
{
    public class ToDoItemBuilder
    {
        private ToDoItem _todo = new ToDoItem();

        public ToDoItemBuilder Id(int id)
        {
            _todo = new ToDoItem(id);
            return this;
        }

        public ToDoItemBuilder Title(string title)
        {
            _todo.Title = title;
            return this;
        }

        public ToDoItemBuilder Description(string description)
        {
            _todo.Description = description;
            return this;
        }

        public ToDoItemBuilder WithDefaultValues()
        {
            _todo = new ToDoItem(1) { Title = "Test Item", Description = "Test Description" };
            return this;
        }

        public ToDoItem Build() => _todo;
    }
}
