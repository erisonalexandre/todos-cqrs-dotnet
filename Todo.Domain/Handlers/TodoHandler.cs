using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler :
        Notifiable,
        IHandler<CreateTodoCommand>,
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>,
        IHandler<DeleteTodoCommand>
    {
        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            var todo = new TodoItem(command.Title, command.Date, command.User);

            _repository.Create(todo);

            return new GenericCommandResult(true, "Tarefa salva com sucesso!", todo);
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            var todo = _repository.GetById(command.Id, command.User);

            if (todo == null)
            {
                return new GenericCommandResult(false, "Tarefa não encontrada!", new { });
            }

            todo.UpdateTitle(command.Title);

            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa atualizada com sucesso!", todo);
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            var todo = _repository.GetById(command.Id, command.User);

            if (todo == null)
            {
                return new GenericCommandResult(false, "Tarefa não encontrada!", new { });
            }

            todo.MarkAsDone();

            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva com sucesso!", todo);
        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            var todo = _repository.GetById(command.Id, command.User);


            if (todo == null)
            {
                return new GenericCommandResult(false, "Tarefa não encontrada!", new { });
            }

            todo.MarkAsUndone();

            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva com sucesso!", todo);
        }

        public ICommandResult Handle(DeleteTodoCommand command)
        {
            command.Validate();
            if (command.Invalid)
            {
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", command.Notifications);
            }

            var todo = _repository.GetById(command.Id, command.User);

            if (todo == null)
            {
                return new GenericCommandResult(false, "Tarefa não encontrada!", new { });
            }

            _repository.Delete(todo);

            return new GenericCommandResult(true, "Tarefa removida com sucesso!", todo);
        }
    }
}
