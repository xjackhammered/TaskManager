using Microsoft.VisualBasic;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isCompleted { get; set; }
    }
}
