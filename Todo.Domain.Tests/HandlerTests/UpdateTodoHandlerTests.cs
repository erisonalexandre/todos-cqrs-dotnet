using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class UpdateTodoHandlerTests
    {
        private readonly UpdateTodoCommand _invalidCommand = new UpdateTodoCommand(Guid.NewGuid(), "", "marcos");
        private readonly UpdateTodoCommand _validCommand = new UpdateTodoCommand(Guid.NewGuid(), "Título da tarefa", "marcos");
        private readonly TodoHandler _handler = new TodoHandler(new FakeTodoRepository());

        public UpdateTodoHandlerTests()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [TestMethod]
        public void Given_a_valid_command_must_update_the_task()
        {
            var result = (GenericCommandResult)_handler.Handle(_validCommand);
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public void Given_an_invalid_command_must_not_update_the_task()
        {
            var result = (GenericCommandResult)_handler.Handle(_invalidCommand);
            Assert.AreEqual(result.Success, false);
        }
    }
}
