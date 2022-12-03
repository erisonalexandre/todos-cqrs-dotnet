using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests
{

    [TestClass]
    public class TodoQueryTests
    {
        private List<TodoItem> _items;

        public TodoQueryTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Título aqui", DateTime.Now, "User"));
            _items.Add(new TodoItem("Título aqui 2", DateTime.Now, "User"));
            _items.Add(new TodoItem("Título aqui 3", DateTime.Now, "User"));
            _items.Add(new TodoItem("Título aqui 4", DateTime.Now, "User"));
            _items.Add(new TodoItem("Título aqui 5", DateTime.Now, "User"));
            _items.Add(new TodoItem("Título aqui 6", DateTime.Now, "User"));
            _items.Add(new TodoItem("Título aqui 7", DateTime.Now, "Erison"));
            _items.Add(new TodoItem("Título aqui 8", DateTime.Now, "Erison"));
            _items.Add(new TodoItem("Título aqui 9", DateTime.Now, "Erison"));
            _items.Add(new TodoItem("Título aqui 10", DateTime.Now, "Erison"));

            var item = new TodoItem("Título aqui 11", DateTime.Now, "Erison");
            item.MarkAsDone();
            _items.Add(item);
        }

        [TestMethod]
        public void Given_a_query_must_return_only_from_user_erison()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("Erison"));
            Assert.AreEqual(result.Count(), 5);
        }

        [TestMethod]
        public void Given_a_query_must_return_only_from_user_user()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("User"));
            Assert.AreEqual(result.Count(), 6);
        }

        [TestMethod]
        public void Given_a_query_must_return_only_done_tasks_from_user_erison()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAllDone("Erison"));
            Assert.AreEqual(result.Count(), 1);
        }

        [TestMethod]
        public void Given_a_query_must_return_only_undone_tasks_from_user_erison()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAllUndone("Erison"));
            Assert.AreEqual(result.Count(), 4);
        }

        [TestMethod]
        public void Given_a_query_must_return_only_done_tasks_from_user_user()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAllDone("User"));
            Assert.AreEqual(result.Count(), 0);
        }

        [TestMethod]
        public void Given_a_query_must_return_only_undone_tasks_from_user_user()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAllUndone("User"));
            Assert.AreEqual(result.Count(), 6);
        }
    }
}
