﻿using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Queries;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _context;

        public TodoRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(TodoItem todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public void Update(TodoItem todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public TodoItem? GetById(Guid id, string user)
        {
            return _context.Todos.FirstOrDefault(x => x.Id == id && x.User == user);
        }

        public IEnumerable<TodoItem> GetAll(string user)
        {
            return _context.Todos.Where(TodoQueries.GetAll(user)).OrderBy(x => x.Date).AsNoTracking().ToList();
        }

        public IEnumerable<TodoItem> GetAllDone(string user)
        {
            return _context.Todos.Where(TodoQueries.GetAllDone(user)).OrderBy(x => x.Date).AsNoTracking().ToList();
        }

        public IEnumerable<TodoItem> GetAllUndone(string user)
        {
            return _context.Todos.Where(TodoQueries.GetAllUndone(user)).OrderBy(x => x.Date).AsNoTracking().ToList();
        }

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done)
        {
            return _context.Todos.Where(TodoQueries.GetByPeriod(user, date, done)).OrderBy(x => x.Date).AsNoTracking().ToList();
        }

        public void Delete(Guid id, string user)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.Id == id && x.User == user);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                _context.SaveChanges();
            }
        }
    }
}