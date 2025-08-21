using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isCompleted { get; set; }
        public TaskDetails? Details { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
