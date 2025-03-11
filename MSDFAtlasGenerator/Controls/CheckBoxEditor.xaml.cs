using System.Windows;
using MSDFAtlasGenerator.Contracts;

namespace MSDFAtlasGenerator.Controls;

public partial class CheckBoxEditor : Editor
{
    public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof(IsChecked),
                                                                                              typeof(bool),
                                                                                              typeof(CheckBoxEditor),
                                                                                              new PropertyMetadata(false));

    public CheckBoxEditor()
    {
        InitializeComponent();
    }

    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }
}
