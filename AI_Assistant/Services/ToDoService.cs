using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI_Assistant.Models;

namespace AI_Assistant.Services
{
    public class ToDoService
    {
        private readonly List<ToDoItem> _toDoItems;

        public ToDoService()
        {
            _toDoItems = new List<ToDoItem>();
        }

        public void AddToDoItem(ToDoItem item)
        {
            _toDoItems.Add(item);
        }

        public void RemoveToDoItem(ToDoItem item)
        {
            _toDoItems.Remove(item);
        }

        public List<ToDoItem> GetToDoItems()
        {
            return _toDoItems.OrderByDescending(item => item.CreatedAt).ToList();
        }

        public void MarkAsCompleted(ToDoItem item)
        {
            var toDoItem = _toDoItems.FirstOrDefault(i => i == item);
            if (toDoItem != null)
            {
                toDoItem.IsCompleted = true;
            }
        }

        public List<ToDoItem> GetPendingItems()
        {
            return _toDoItems.Where(item => !item.IsCompleted).ToList();
        }

    }
}
