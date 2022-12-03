using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;

namespace Todo.Domain.Tests.EntitiesTests
{
    [TestClass]
    public class TodoItemTests
    {
        private readonly TodoItem _validTodo = new TodoItem("Título aqui", DateTime.Now, "User");

        [TestMethod]
        public void Given_a_new_task_the_task_must_be_incomplete()
        {
            Assert.AreEqual(_validTodo.Done, false);
        }

        [TestMethod]
        public void Given_a_new_task_the_task_must_be_created_today()
        {
            Assert.AreEqual(_validTodo.Date.Date, DateTime.Now.Date);
        }

        [TestMethod]
        public void Given_a_new_task_the_task_must_be_created_by_the_user()
        {
            Assert.AreEqual(_validTodo.User, "User");
        }

        [TestMethod]
        public void Given_a_new_task_the_task_must_have_a_title()
        {
            Assert.AreEqual(_validTodo.Title, "Título aqui");
        }
    }
}
