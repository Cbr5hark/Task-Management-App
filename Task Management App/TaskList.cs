using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_App
{
    public class TaskList
    {
        public string Title { get; set; }
        public List<Task> Tasks { get; private set; }

        public TaskList(string name)
        {
            Title = name;
            Tasks = new List<Task>();
        }

        public void AddTask(string title, string description, TaskStatus status, DateTime dueDate)
        {
            Tasks.Add(new Task(title, description, status, dueDate));
        }

        public void RemoveTask(int id)
        {
            Task task = Tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                Tasks.Remove(task);
            }
        }
    }
}
