using System.Windows;
using MSDFAtlasGenerator.Contracts;

namespace MSDFAtlasGenerator.Controls;

/// <summary>
/// CheckBoxEdit.xaml 的交互逻辑
/// </summary>
public partial class CheckBoxEdit : Edit
{
    public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof(IsChecked),
                                                                                              typeof(bool),
                                                                                              typeof(CheckBoxEdit),
                                                                                              new PropertyMetadata(false));

    public CheckBoxEdit()
    {
        InitializeComponent();
    }

    public bool IsChecked
    {
        get { return (bool)GetValue(IsCheckedProperty); }
        set { SetValue(IsCheckedProperty, value); }
    }
}
