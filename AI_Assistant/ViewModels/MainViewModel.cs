using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AI_Assistant.Models;
using AI_Assistant.Services;

namespace AI_Assistant.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ToDoItem> ToDoItems { get; set; }
        private readonly ToDoService _toDoService;
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand MarkAsCompletedCommand { get; }

        public MainViewModel()
        {
            _toDoService = new ToDoService();
            ToDoItems = new ObservableCollection<ToDoItem>(_toDoService.GetToDoItems());
            AddCommand = new RelayCommand(AddToDoItem);
            RemoveCommand = new RelayCommand(RemoveToDoItem);
            MarkAsCompletedCommand = new RelayCommand(MarkAsCompleted);
        }

        private void AddToDoItem(object parameter)
        {
            var newItem = new ToDoItem
            {
                Title = "New Task",
                Description = "Task Description",
                CreatedAt = DateTime.Now
            };
            _toDoService.AddToDoItem(newItem);
            ToDoItems.Remove(newItem);
            OnPropertyChanged("ToDoItems");

        }

        private void RemoveToDoItem(object parameter) {
    }

}
