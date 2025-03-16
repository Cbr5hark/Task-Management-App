using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_App
{
    public class Task
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }

        public Task(string title, string description, TaskStatus status, DateTime dueDate)
        {
            Id = _nextId++;
            Title = title;
            Description = description;
            Status = status;
            DueDate = dueDate;
        }
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}
