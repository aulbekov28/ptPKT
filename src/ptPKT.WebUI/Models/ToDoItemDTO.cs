using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;
using ptPKT.Core.Entities;
using ptPKT.Core.Entities.BL;

namespace ptPKT.WebUI.Models
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; private set; }

        public static ToDoItemDTO FromToDoItem(ToDoItem item)
        {
            return new ToDoItemDTO()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsDone = item.IsDone
            };
        }

        public ToDoItem ToTodoItem()
        {
            return new ToDoItem()
            {
                Title = Title,
                Description = Description,
            };
        }
    }
}