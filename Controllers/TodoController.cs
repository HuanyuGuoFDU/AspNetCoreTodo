using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

using AspNetCoreTodo.Services;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            
            // Get to-do items from database
            var items = await _todoItemService.GetIncompleteItemsAsync(currentUser);

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

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            //Add input validation to ensure that StartDate is not after DueDate.
            if (newItem.StartDate > newItem.DueDate)
            {
                return BadRequest("Due date must be after start date.");
            }

            var successful = await _todoItemService.AddItemAsync(newItem, currentUser);
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

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _todoItemService.MarkDoneAsync(id, currentUser);
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

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            Console.WriteLine("item id: " + updatedItem.Id);

            Type type = updatedItem.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties) {
                Console.WriteLine("{0} = {1}", property.Name, property.GetValue(updatedItem) ?? "null");
            }

            // update database by updatedItem.Id when the of updatedItem is not null
            var successful = await _todoItemService.UpdateItemAsync(updatedItem, currentUser);
            
            if (!successful)
            {
                Console.WriteLine("item id: " + updatedItem.Id + "Could not add item.");
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }

    }
}