using System.ComponentModel.DataAnnotations;

namespace TaskManagment.Models.Dtos
{
    public class TaskDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime DueDate { get; set; }
    }
}
