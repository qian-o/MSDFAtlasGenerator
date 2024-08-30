using System.Windows;
using System.Windows.Controls;
using MSDFAtlasGenerator.Models;

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
    }

    public OutputData? OutputData
    {
        get { return (OutputData)GetValue(OutputDataProperty); }
        set { SetValue(OutputDataProperty, value); }
    }
}
