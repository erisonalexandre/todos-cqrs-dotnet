﻿using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands
{
    public class DeleteTodoCommand : Notifiable, ICommand
    {
        public DeleteTodoCommand(Guid id, string user)
        {
            Id = id;
            User = user;
        }

        public Guid Id { get; set; }

        public string User { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasLen(Id.ToString(), 36, "Id", "Tamanho de id inválido")
                    .HasMinLen(User, 6, "User", "Usuário inválido")
            );
        }
    }
}
