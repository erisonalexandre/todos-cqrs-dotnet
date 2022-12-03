using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands
{
    public class UpdateTodoCommand : Notifiable, ICommand
    {
        public UpdateTodoCommand()
        {

        }

        public UpdateTodoCommand(Guid id, string title, string user)
        {
            Id = id;
            Title = title;
            User = user;
        }


        public Guid Id { get; set; }

        public string Title { get; set; }

        public string User { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Title, 3, "Title", "Por favor, descreva melhor essa tarefa!")
                    .HasLen(Id.ToString(), 36, "Id", "Tamanho de id inválido")
                    .HasMinLen(User, 6, "User", "Usuário inválido")
            );
        }
    }
}
