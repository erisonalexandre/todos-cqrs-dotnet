using Todo.Domain.Commands;

namespace Todo.Domain.Tests.CommandTests;

[TestClass]
public class CreateTodoCommandTests
{
    private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "marcos", DateTime.Now);
    private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Título da tarefa", "marcos", DateTime.Now);

    public CreateTodoCommandTests()
    {
        _invalidCommand.Validate();
        _validCommand.Validate();
    }

    [TestMethod]
    public void Given_a_valid_command_must_create_the_task()
    {
        Assert.AreEqual(_validCommand.Valid, true);
    }

    [TestMethod]
    public void Given_an_invalid_command_must_not_create_the_task()
    {
        Assert.AreEqual(_invalidCommand.Valid, false);
    }
}