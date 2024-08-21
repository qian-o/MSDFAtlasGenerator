using System.Windows;
using Microsoft.Win32;
using MSDFAtlasGenerator.Contracts;

namespace MSDFAtlasGenerator.Controls;

/// <summary>
/// OpenFileEdit.xaml 的交互逻辑
/// </summary>
public partial class OpenFileEdit : Edit
{
    public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(nameof(Filter),
                                                                                           typeof(string),
                                                                                           typeof(OpenFileEdit),
                                                                                           new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty IsMultiselectProperty = DependencyProperty.Register(nameof(IsMultiselect),
                                                                                                 typeof(bool),
                                                                                                 typeof(OpenFileEdit),
                                                                                                 new PropertyMetadata(false));

    public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(nameof(FilePath),
                                                                                             typeof(string),
                                                                                             typeof(OpenFileEdit),
                                                                                             new PropertyMetadata(string.Empty));

    public OpenFileEdit()
    {
        InitializeComponent();
    }

    public string Filter
    {
        get { return (string)GetValue(FilterProperty); }
        set { SetValue(FilterProperty, value); }
    }

    public bool IsMultiselect
    {
        get { return (bool)GetValue(IsMultiselectProperty); }
        set { SetValue(IsMultiselectProperty, value); }
    }

    public string FilePath
    {
        get { return (string)GetValue(FilePathProperty); }
        set { SetValue(FilePathProperty, value); }
    }

    private void BrowseFile_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = Filter,
            Multiselect = IsMultiselect
        };

        if (openFileDialog.ShowDialog() == true)
        {
            FilePath = openFileDialog.FileName;
        }
    }
}
