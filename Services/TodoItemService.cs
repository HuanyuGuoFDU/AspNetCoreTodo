using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            var items = await _context.Items
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
            return items;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;


            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item = await _context.Items
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.IsDone = true;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }

        public async Task<bool> UpdateItemAsync(TodoItem UpdateItem)
        {
            var item = await _context.Items
                .Where(x => x.Id == UpdateItem.Id)
                .SingleOrDefaultAsync();

            if (item == null) return false;

            item.Title = UpdateItem.Title == null ? item.Title : UpdateItem.Title;
            item.DueDate = UpdateItem.DueDate == null ? item.DueDate : UpdateItem.DueDate;
            item.StartDate = UpdateItem.StartDate == null ? item.StartDate : UpdateItem.StartDate;
            item.NumberOfDays = UpdateItem.NumberOfDays == 0 ? item.NumberOfDays : UpdateItem.NumberOfDays;
            item.Priority = UpdateItem.Priority == 0 ? item.Priority : UpdateItem.Priority;

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

    }
}