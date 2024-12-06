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

        private void RemoveToDoItem(object parameter)
        {
            var item = parameter as ToDoItem;
            if (item != null)
            {
                _toDoService.RemoveToDoItem(item);
                ToDoItems.Remove(item);
                OnPropertyChanged("ToDoItems");
            }
        }

        private void MarkAsCompleted(object parameter)
        {
            var item = parameter as ToDoItem;
            if (item != null)
            {
                _toDoService.MarkAsCompleted(item);
                OnPropertyChanged("ToDoItems");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;
            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}
