using System.Windows;
using Microsoft.Win32;
using MSDFAtlasGenerator.Contracts;

namespace MSDFAtlasGenerator.Controls;

public partial class OpenFileEditor : Editor
{
    public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(nameof(Filter),
                                                                                           typeof(string),
                                                                                           typeof(OpenFileEditor),
                                                                                           new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty IsMultiselectProperty = DependencyProperty.Register(nameof(IsMultiselect),
                                                                                                 typeof(bool),
                                                                                                 typeof(OpenFileEditor),
                                                                                                 new PropertyMetadata(false));

    public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(nameof(FilePath),
                                                                                             typeof(string),
                                                                                             typeof(OpenFileEditor),
                                                                                             new PropertyMetadata(string.Empty));

    public OpenFileEditor()
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
