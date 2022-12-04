using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repository;
        private readonly TodoHandler _handler;

        public TodoController(ITodoRepository repository, TodoHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _repository.GetAll("Erison");
        }

        [HttpGet]
        [Route("done")]
        public IEnumerable<TodoItem> GetAllDone()
        {
            return _repository.GetAllDone("Erison");
        }

        [HttpGet]
        [Route("undone")]
        public IEnumerable<TodoItem> GetAllUndone()
        {
            return _repository.GetAllUndone("Erison");
        }

        [HttpGet]
        [Route("done/today")]
        public IEnumerable<TodoItem> GetDoneForToday()
        {
            return _repository.GetByPeriod("Erison", DateTime.Now.Date, true);
        }

        [HttpGet]
        [Route("undone/today")]
        public IEnumerable<TodoItem> GetUndoneForToday()
        {
            return _repository.GetByPeriod("Erison", DateTime.Now.Date, false);
        }

        [HttpGet]
        [Route("done/tomorrow")]
        public IEnumerable<TodoItem> GetDoneForTomorrow()
        {
            return _repository.GetByPeriod("Erison", DateTime.Now.Date.AddDays(1), true);
        }

        [HttpGet]
        [Route("undone/tomorrow")]
        public IEnumerable<TodoItem> GetUndoneForTomorrow()
        {
            return _repository.GetByPeriod("Erison", DateTime.Now.Date.AddDays(1), false);
        }

        [HttpGet]
        [Route("{id}")]
        public TodoItem? GetById(Guid id)
        {
            return _repository.GetById(id, "Erison");
        }

        [HttpPost]
        public GenericCommandResult Create([FromBody] CreateTodoCommand command)
        {
            command.User = "Erison";
            return (GenericCommandResult)_handler.Handle(command);
        }

        [HttpPut]
        public GenericCommandResult Update([FromBody] UpdateTodoCommand command)
        {
            command.User = "Erison";
            return (GenericCommandResult)_handler.Handle(command);
        }

        [HttpPut]
        [Route("mark-as-done")]
        public GenericCommandResult MarkAsDone([FromBody] MarkTodoAsDoneCommand command)
        {
            command.User = "Erison";
            return (GenericCommandResult)_handler.Handle(command);
        }

        [HttpPut]
        [Route("mark-as-undone")]
        public GenericCommandResult MarkAsUndone([FromBody] MarkTodoAsUndoneCommand command)
        {
            command.User = "Erison";
            return (GenericCommandResult)_handler.Handle(command);
        }

        [HttpDelete]
        [Route("{id}")]
        public GenericCommandResult Delete(Guid id)
        {
            var command = new DeleteTodoCommand(id, "Erison");
            return (GenericCommandResult)_handler.Handle(command);
        }


    }
}
