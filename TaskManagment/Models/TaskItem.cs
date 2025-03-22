using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime DueDate { get; set; }
    }
}
