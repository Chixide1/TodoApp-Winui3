using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Text;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TodoApp;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Home : Page
{
    private readonly ObservableCollection<Todo> _items = [
        new("Morning workout"),
        new("Check emails"),
        new("Project task"),
        new("Grocery shopping"),
        new("Read or learn"),
        new("Evening reflection")
    ];

    public Home()
    {
        InitializeComponent();
    }

    private void AddTodoButton_Click(object sender, RoutedEventArgs e)
    {
        _items.Add(new(AddTodoInput.Text));
        AddTodoInput.Text = string.Empty;
    }

    private void AddTodoInput_KeyUp(object sender, KeyRoutedEventArgs e)
    {
        if(e.Key == VirtualKey.Enter)
        {
            AddTodoButton_Click(new(), new());
        }
    }

    private void DeleteTodoButton_Click(object sender, RoutedEventArgs e)
    {
        if(sender is Button deleteButton && deleteButton.DataContext is Todo item)
        {
            _items.Remove(item);
        }
    }
}

public partial class Todo : ObservableObject
{
    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private bool done;

    public Todo() { }

    public Todo(string title)
    {
        Title = title;
    }
}

public class BoolToStrikethroughConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if(value is bool done)
        {
            return done ? TextDecorations.Strikethrough : TextDecorations.None;
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if(value is TextDecorations textDecorations)
        {
            return textDecorations == TextDecorations.Strikethrough;
        }

        return value;
    }
}