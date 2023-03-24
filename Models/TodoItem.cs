using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class TodoItem : IComparable<TodoItem>
    {
        public int CompareTo(TodoItem? other)
        {
            if (other !=  null)
            {
                return this.Priority - other.Priority;
            } else {
                return 1;
            }
        }

        public Guid Id { get; set; }

        public bool IsDone { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset? DueAt { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public int NumberOfDays { get; set; }

        public int Priority { get; set; }

    }
}