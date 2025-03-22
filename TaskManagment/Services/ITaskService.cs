using TaskManagment.Models.Dtos;
using TaskManagment.Models;

namespace TaskManagment.Services
{
    public interface ITaskService
    {
        TaskItem CreateTask(TaskDto taskDto);
        List<TaskItem> GetAllTasks();
        TaskItem? GetTaskById(int id);
        TaskItem? UpdateTask(int id, TaskDto taskDto);
        bool DeleteTask(int id);
    }
}
