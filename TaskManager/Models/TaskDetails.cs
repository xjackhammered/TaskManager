using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class TaskDetails
    {
        public int Id { get; set; }
        public string Note { get; set; } = string.Empty;
        
        public int TaskItemId { get; set; }
        [ForeignKey("TaskItemId")]
        public TaskItem? TaskItem { get; set; }

    }   
}
