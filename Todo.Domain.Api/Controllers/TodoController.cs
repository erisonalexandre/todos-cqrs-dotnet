using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    [Authorize]
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
            string? user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return _repository.GetAll(user);
        }

        [HttpGet]
        [Route("done")]
        public IEnumerable<TodoItem> GetAllDone()
        {
            string? user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return _repository.GetAllDone(user);
        }

        [HttpGet]
        [Route("undone")]
        public IEnumerable<TodoItem> GetAllUndone()
        {
            string? user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return _repository.GetAllUndone(user);
        }

        [HttpGet]
        [Route("done/today")]
        public IEnumerable<TodoItem> GetDoneForToday()
        {
            string? user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return _repository.GetByPeriod("Erison", DateTime.Now.Date, true);
        }

        [HttpGet]
        [Route("undone/today")]
        public IEnumerable<TodoItem> GetUndoneForToday()
        {
            string? user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return _repository.GetByPeriod("Erison", DateTime.Now.Date, false);
        }

        [HttpGet]
        [Route("done/tomorrow")]
        public IEnumerable<TodoItem> GetDoneForTomorrow()
        {
            string? user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return _repository.GetByPeriod("Erison", DateTime.Now.Date.AddDays(1), true);
        }

        [HttpGet]
        [Route("undone/tomorrow")]
        public IEnumerable<TodoItem> GetUndoneForTomorrow()
        {
            string? user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return _repository.GetByPeriod("Erison", DateTime.Now.Date.AddDays(1), false);
        }

        [HttpGet]
        [Route("{id}")]
        public TodoItem? GetById(Guid id)
        {
            string? user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return _repository.GetById(id, "Erison");
        }

        [HttpPost]
        public GenericCommandResult Create([FromBody] CreateTodoCommand command)
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)_handler.Handle(command);
        }

        [HttpPut]
        public GenericCommandResult Update([FromBody] UpdateTodoCommand command)
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)_handler.Handle(command);
        }

        [HttpPut]
        [Route("mark-as-done")]
        public GenericCommandResult MarkAsDone([FromBody] MarkTodoAsDoneCommand command)
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)_handler.Handle(command);
        }

        [HttpPut]
        [Route("mark-as-undone")]
        public GenericCommandResult MarkAsUndone([FromBody] MarkTodoAsUndoneCommand command)
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)_handler.Handle(command);
        }

        [HttpDelete]
        [Route("{id}")]
        public GenericCommandResult Delete([FromBody] DeleteTodoCommand command)
        {
            command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return (GenericCommandResult)_handler.Handle(command);
        }


    }
}
