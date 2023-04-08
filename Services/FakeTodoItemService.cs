using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            var item1 = new TodoItem
            {
                Title = "Learn ASP.NET Core",
                DueDate = DateTimeOffset.Now.AddDays(1),
                StartDate = DateTimeOffset.Now.AddDays(-5),
                NumberOfDays = 10,
                Priority = 1
            };

            var item2 = new TodoItem
            {
                Title = "Build awesome apps",
                DueDate = DateTimeOffset.Now.AddDays(2),
                StartDate = DateTimeOffset.Now.AddDays(-5),
                NumberOfDays = 10,
                Priority = 2
            };

            var item3 = new TodoItem
            {
                Title = "Sort mork Task 1",
                StartDate = DateTimeOffset.Now.AddDays(-5),
                NumberOfDays = 10,
                Priority = 5
            };

            var item4 = new TodoItem
            {
                Title = "Sort mork Task 2",
                DueDate = DateTimeOffset.Now.AddDays(2),
                NumberOfDays = 3,
                Priority = 3
            };

            var item5 = new TodoItem
            {
                Title = "Sort mork Task 3",
                DueDate = DateTimeOffset.Now.AddHours(12),
                StartDate = DateTimeOffset.Now.AddHours(-12),
                NumberOfDays = 1,
                Priority = 4
            };

            var item6 = new TodoItem
            {
                Title = "Sort mork Task 4",
                DueDate = DateTimeOffset.Now.AddDays(2),
                StartDate = DateTimeOffset.Now.AddDays(-5),
                NumberOfDays = 10,
                Priority = 3
            };

            var item7 = new TodoItem
            {
                Title = "Sort mork Task 5",
                DueDate = DateTimeOffset.Now.AddDays(2),
                StartDate = DateTimeOffset.Now.AddDays(-3),
                NumberOfDays = 3,
                Priority = 4
            };

            var item8 = new TodoItem
            {
                Title = "Sort mork Task 6",
                DueDate = DateTimeOffset.Now.AddHours(12),
                StartDate = DateTimeOffset.Now.AddHours(-12),
                NumberOfDays = 1,
                Priority = 1
            };

            return Task.FromResult(new[] { item1, item2, item3, item4, item5, item6, item7, item8 });
        }

        public Task<bool> AddItemAsync(TodoItem newItem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkDoneAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}