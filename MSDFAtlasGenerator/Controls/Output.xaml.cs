using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MSDFAtlasGenerator.Helpers;
using MSDFAtlasGenerator.Models;
using Wpf.Ui.Controls;

namespace MSDFAtlasGenerator.Controls;

public partial class Output : UserControl
{
    public static readonly DependencyProperty OutputDataProperty = DependencyProperty.Register(nameof(OutputData),
                                                                                               typeof(OutputData),
                                                                                               typeof(Output),
                                                                                               new PropertyMetadata(null));

    public Output()
    {
        InitializeComponent();

        Loaded += (_, _) =>
        {
            if (OutputData != null && VisualTreeHelpers.GetChildByType<ScrollViewer>(this) is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToBottom();
            }
        };
    }

    public OutputData? OutputData
    {
        get { return (OutputData)GetValue(OutputDataProperty); }
        set { SetValue(OutputDataProperty, value); }
    }

    private void ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (e.ExtentHeightChange > 0.0)
        {
            ((ScrollViewer)e.OriginalSource).ScrollToBottom();
        }
    }

    private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Clipboard.SetText(((Log)((FrameworkElement)sender).DataContext).Message);

        App.SnackbarService.Show("Copied to clipboard",
                                 "The message has been copied to the clipboard.",
                                 ControlAppearance.Secondary,
                                 null,
                                 App.SnackbarService.DefaultTimeOut);
    }
}
