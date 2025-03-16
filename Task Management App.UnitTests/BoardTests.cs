using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Task_Management_App.UnitTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void CreateBoard_WithName_SetsNameCorrectly()
        {
            string boardName = "Test Board";
            Board board = new Board(boardName);

            Assert.AreEqual(boardName, board.Name);
            Assert.IsNotNull(board.Lists);
            Assert.AreEqual(0, board.Lists.Count);
        }

        [TestMethod]
        public void AddList_WithTitle_AddsListCorrectly()
        {
            Board board = new Board("Test Board");
            string listTitle = "Test List";

            board.AddList(listTitle);

            Assert.AreEqual(1, board.Lists.Count);
            Assert.AreEqual(listTitle, board.Lists[0].Title);
        }

        [TestMethod]
        public void RemoveList_ExistingList_RemovesListCorrectly()
        {
            Board board = new Board("Test Board");
            string listTitle = "Test List";
            board.AddList(listTitle);

            board.RemoveList(listTitle);

            Assert.AreEqual(0, board.Lists.Count);
        }

        [TestMethod]
        public void RemoveList_NonExistingList_DoesNothing()
        {
            Board board = new Board("Test Board");
            board.AddList("Test List");

            board.RemoveList("Non-existent List");

            Assert.AreEqual(1, board.Lists.Count);
        }

        [TestMethod]
        public void GetList_ExistingList_ReturnsCorrectList()
        {
            Board board = new Board("Test Board");
            string listTitle = "Test List";
            board.AddList(listTitle);

            TaskList list = board.GetList(listTitle);

            Assert.IsNotNull(list);
            Assert.AreEqual(listTitle, list.Title);
        }

        [TestMethod]
        public void GetList_NonExistingList_ReturnsNull()
        {
            Board board = new Board("Test Board");

            TaskList list = board.GetList("Non-existent List");

            Assert.IsNull(list);
        }
    }
}