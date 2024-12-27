using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Linq;


namespace MyTodoList;

public partial class MainPage : ContentPage
{
    public ObservableCollection<TodoItem> TodoItems { get; } = new();

    public MainPage()
    {
        InitializeComponent();
        TodoListCollectionView.ItemsSource = TodoItems;
        LoadTodos();
    }

    private void LoadTodos()
    {
        
        TodoItems.Add(new TodoItem { Title = "Купить хлеб", IsCompleted = false });
        TodoItems.Add(new TodoItem { Title = "Позвонить маме", IsCompleted = true });
    }

    private void AddTodoButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NewTodoEntry.Text)) return;

        TodoItems.Add(new TodoItem { Title = NewTodoEntry.Text, IsCompleted = false });
        NewTodoEntry.Text = string.Empty;
    }


    private void DeleteTodoButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is TodoItem item)
        {
            TodoItems.Remove(item);

        }
    }

    public class TodoItem : INotifyPropertyChanged
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(TitleText));
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (_isCompleted == value) return;
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(TitleText));
            }
        }
        public string TitleText
        {
            get
            {
                return $"{Title} - {(IsCompleted ? "Выполнено" : "Не выполнено")}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}