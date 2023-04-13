using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public async Task<IActionResult> Index()
        {
            // Get to-do items from database
            var items = await _todoItemService.GetIncompleteItemsAsync();

            // sort the todoitem by priority
            Array.Sort<TodoItem>(items);

            // Put items into a model
            var model = new TodoViewModel()
            {
                Items = items
            };

            // Render view using the model
            return View(model);           
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            //Add input validation to ensure that StartDate is not after DueDate.
            if (newItem.StartDate > newItem.DueDate)
            {
                return BadRequest("Due date must be after start date.");
            }

            var successful = await _todoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }

            var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItem(TodoItem updatedItem)
        {
            // When modifying non-title values, title should be null, so here didn't use ModelState.IsValid
            if (updatedItem.Id == null)
            {

                return RedirectToAction("Index");
            }

            Console.WriteLine("item id: " + updatedItem.Id);

            Type type = updatedItem.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties) {
                Console.WriteLine("{0} = {1}", property.Name, property.GetValue(updatedItem) ?? "null");
            }

            //TODO update database by updatedItem.Id when the filed of updatedItem is not null
            var successful = await _todoItemService.UpdateItemAsync(updatedItem);
            
            if (!successful)
            {
                Console.WriteLine("item id: " + updatedItem.Id + "Could not add item.");
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

    }
}