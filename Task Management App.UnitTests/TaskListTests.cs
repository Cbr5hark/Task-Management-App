using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task_Management_App.UnitTests
{
    [TestClass]
    public class TaskListTests
    {
        [TestMethod]
        public void CreateTaskList_WithName_SetsNameCorrectly()
        {
            string listName = "Test List";
            TaskList taskList = new TaskList(listName);

            Assert.AreEqual(listName, taskList.Title);
            Assert.IsNotNull(taskList.Tasks);
            Assert.AreEqual(0, taskList.Tasks.Count);
        }

        [TestMethod]
        public void AddTask_WithValidParameters_AddsTaskCorrectly()
        {
            TaskList taskList = new TaskList("Test List");
            string taskTitle = "Test Task";
            string description = "Test Description";
            TaskStatus status = TaskStatus.ToDo;
            DateTime dueDate = DateTime.Now.AddDays(1);

            taskList.AddTask(taskTitle, description, status, dueDate);

            Assert.AreEqual(1, taskList.Tasks.Count);
            Assert.AreEqual(taskTitle, taskList.Tasks[0].Title);
            Assert.AreEqual(description, taskList.Tasks[0].Description);
            Assert.AreEqual(status, taskList.Tasks[0].Status);
            Assert.AreEqual(dueDate, taskList.Tasks[0].DueDate);
        }

        [TestMethod]
        public void RemoveTask_ExistingTask_RemovesTaskCorrectly()
        {
            TaskList taskList = new TaskList("Test List");
            taskList.AddTask("Test Task", "Description", TaskStatus.ToDo, DateTime.Now);
            int taskId = taskList.Tasks[0].Id;

            taskList.RemoveTask(taskId);

            Assert.AreEqual(0, taskList.Tasks.Count);
        }

        [TestMethod]
        public void RemoveTask_NonExistingTask_DoesNothing()
        {
            TaskList taskList = new TaskList("Test List");
            taskList.AddTask("Test Task", "Description", TaskStatus.ToDo, DateTime.Now);

            taskList.RemoveTask(999);

            Assert.AreEqual(1, taskList.Tasks.Count);
        }
    }
}