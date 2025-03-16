using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Management_App
{
    public class Board
    {
        public string Name { get; set; }
        public List<TaskList> Lists { get; private set; }

        public Board(string name)
        {
            Name = name;
            Lists = new List<TaskList>();
        }

        public void AddList(string title)
        {
            Lists.Add(new TaskList(title));
        }

        public void RemoveList(string title)
        {
            TaskList list = Lists.FirstOrDefault(l => l.Title == title);
            if (list != null)
            {
                Lists.Remove(list);
            }
        }

        public TaskList GetList(string title)
        {
            return Lists.FirstOrDefault(l => l.Title == title);
        }
    }
}