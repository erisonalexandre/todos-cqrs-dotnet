using System.Linq.Expressions;
using Todo.Domain.Entities;

namespace Todo.Domain.Queries
{
    public static class TodoQueries
    {
        public static Expression<Func<TodoItem, bool>> GetAll(string user)
        {
            return x => x.User == user;
        }

        public static Expression<Func<TodoItem, bool>> GetAllDone(string user)
        {
            return x => x.Done && x.User == user;
        }

        public static Expression<Func<TodoItem, bool>> GetAllUndone(string user)
        {
            return x => !x.Done && x.User == user;
        }

        public static Expression<Func<TodoItem, bool>> GetByPeriod(string user, DateTime date, bool done)
        {
            return x => x.Done == done && x.User == user && x.Date.Date == date.Date;
        }
    }
}
