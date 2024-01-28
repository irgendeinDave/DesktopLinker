using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;

namespace DesktopLinker;

public partial class MainWindow : Window
{
    private string? Name = string.Empty;
    private string? ExecPath = string.Empty;
    private string? IconPath = string.Empty;
    private string? Arguments = string.Empty;
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CreateFileBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // get data from input
        Name = InputName.Text;
        ExecPath = InputBinaryPath.Text;
        IconPath = InputIcon.Text;
        Arguments = InputArguments.Text;
        
        try
        {
            string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}/{Name}.desktop";
            using (File.Create(path)) {}
            using (StreamWriter sw = File.CreateText(path))
            { 
                sw.WriteLine("[Desktop Entry]");
                sw.WriteLine($"Name={Name}");
                sw.WriteLine($"Exec={ExecPath}");
                sw.WriteLine($"Icon={IconPath}");
                sw.WriteLine($"Arguments={Arguments}");
            }
        }
        catch (Exception ex)
        {
            ConsoleError(ex.Message);
        }
    }

    private async void InputBinaryPath_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select your program",
            AllowMultiple = false,
            FileTypeFilter = new [] { FilePickerFileTypes.All }
        });
        if (files.Count == 0)
            return;
        
        ExecPath = files[0].Path.AbsolutePath;
        InputBinaryPath.Text = ExecPath;
    }

    private async void InputIcon_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select an Icon",
            AllowMultiple = false,
            FileTypeFilter = new [] { FilePickerFileTypes.ImageAll }
        });
        if (files.Count == 0)
            return;

        IconPath = files[0].Path.AbsolutePath;
        InputIcon.Text = IconPath;
    }

    #region Console
    private void ClearConsole()
    {
        OutputConsoleMessageType.Text = string.Empty;
        OutputConsoleMessage.Text = string.Empty;
    }

    private void ConsoleInfo(string message)
    {
        OutputConsoleMessageType.Text = string.Empty;
        OutputConsoleMessage.Text = message;
    }

    private void ConsoleWarning(string message)
    {
        OutputConsoleMessageType.Text = "Warning:";
        OutputConsoleMessageType.Foreground = new SolidColorBrush(Colors.Orange);
        OutputConsoleMessage.Text = message;
    }

    private void ConsoleError(string message)
    {
        OutputConsoleMessageType.Text = "Error:";
        OutputConsoleMessageType.Foreground = new SolidColorBrush(Colors.Red);
        OutputConsoleMessage.Text = message;
    }
    #endregion
}