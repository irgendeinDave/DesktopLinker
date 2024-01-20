using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

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
        Name = InputName.Text;
        ExecPath = InputBinaryPath.Text;
        IconPath = InputIcon.Text;
        Arguments = InputArguments.Text;
    }
}