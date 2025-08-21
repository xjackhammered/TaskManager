namespace TaskManager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null!;

        public List<TaskItem>? TaskItems { get; set; }
    }
}
