using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Concurrent;
using TaskManagment.Models;
using TaskManagment.Models.Dtos;

namespace TaskManagment.Services
{
    public class TaskService : ITaskService
    {
        private readonly ConcurrentDictionary<int, TaskItem> _tasks = new();
        private int _nextId = 1;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ILogger<TaskService> logger)
        {
            _logger = logger;
        }

        public TaskItem CreateTask(TaskDto taskDto)
        {
            if (taskDto.DueDate < DateTime.Today)
            {
                Log.Warning("Task creation failed: Due date {DueDate} is in the past.", taskDto.DueDate);
                throw new ArgumentException("Due date must be in the future.");
            }

            var task = new TaskItem
            {
                Id = _nextId++,
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate
            };
            _tasks[task.Id] = task;

            _logger.LogInformation("Task {TaskId} created.", task.Id);
            return task;
        }

        public bool DeleteTask(int id)
        {
            if (_tasks.TryRemove(id, out _))
            {
                _logger.LogInformation("Task {TaskId} deleted.", id);
                return true;
            }
            _logger.LogWarning("Task {TaskId} not found for deletion.", id);
            return false;
        }

        public List<TaskItem> GetAllTasks()
        {
            _logger.LogInformation("Retrieving all tasks. Total Count: {TaskCount}", _tasks.Count);
            return _tasks.Values.ToList();
        }

        public TaskItem? GetTaskById(int id)
        {
            if (_tasks.TryGetValue(id, out var task))
            {
                _logger.LogInformation("Task {TaskId} retrieved successfully.", id);
                return task;
            }
            _logger.LogWarning("Task {TaskId} not found.", id);
            return null;
        }

        public TaskItem? UpdateTask(int id, TaskDto taskDto)
        {
            if (!_tasks.ContainsKey(id))
            {
                _logger.LogWarning("Task {TaskId} not found for update.", id);
                return null;
            }

            if (taskDto.DueDate < DateTime.Today)
            {
                Log.Warning("Task update failed: Due date {DueDate} is in the past.", taskDto.DueDate);
                throw new ArgumentException("Due date must be in the future.");
            }

            var updatedTask = new TaskItem
            {
                Id = id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate
            };
            _tasks[id] = updatedTask;

            _logger.LogInformation("Task {TaskId} updated.", id);
            return updatedTask;
        }
    }
}
