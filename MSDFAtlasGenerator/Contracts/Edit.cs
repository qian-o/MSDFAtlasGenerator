using System.Windows;
using System.Windows.Controls;

namespace MSDFAtlasGenerator.Contracts;

public class Edit : UserControl
{
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title),
                                                                                          typeof(string),
                                                                                          typeof(Edit),
                                                                                          new PropertyMetadata(string.Empty));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }
}
