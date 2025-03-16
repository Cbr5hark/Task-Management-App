using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task_Management_App.UnitTests
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void CreateTask_WithValidParameters_SetsPropertiesCorrectly()
        {
            string title = "Test Task";
            string description = "Test Description";
            TaskStatus status = TaskStatus.ToDo;
            DateTime dueDate = DateTime.Now.AddDays(1);

            Task task = new Task(title, description, status, dueDate);

            Assert.IsTrue(task.Id > 0);
            Assert.AreEqual(title, task.Title);
            Assert.AreEqual(description, task.Description);
            Assert.AreEqual(status, task.Status);
            Assert.AreEqual(dueDate, task.DueDate);
        }

        [TestMethod]
        public void CreateTasks_MultipleInstances_AssignsUniqueIds()
        {
            Task task1 = new Task("Task 1", "Description", TaskStatus.ToDo, DateTime.Now);
            Task task2 = new Task("Task 2", "Description", TaskStatus.ToDo, DateTime.Now);
            Task task3 = new Task("Task 3", "Description", TaskStatus.ToDo, DateTime.Now);

            Assert.AreNotEqual(task1.Id, task2.Id);
            Assert.AreNotEqual(task2.Id, task3.Id);
            Assert.AreNotEqual(task1.Id, task3.Id);
        }
    }
}
