using System.Windows;
using MSDFAtlasGenerator.Contracts;

namespace MSDFAtlasGenerator.Controls;

public partial class DragNumberEditor : Editor
{
    public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(nameof(MinValue),
                                                                                             typeof(double),
                                                                                             typeof(DragNumberEditor),
                                                                                             new PropertyMetadata(0.0));

    public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof(MaxValue),
                                                                                             typeof(double),
                                                                                             typeof(DragNumberEditor),
                                                                                             new PropertyMetadata(1.0));

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value),
                                                                                          typeof(double),
                                                                                          typeof(DragNumberEditor),
                                                                                          new PropertyMetadata(0.0));

    public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(nameof(DecimalPlaces),
                                                                                                  typeof(int),
                                                                                                  typeof(DragNumberEditor),
                                                                                                  new PropertyMetadata(2));

    public static readonly DependencyProperty StepProperty = DependencyProperty.Register(nameof(Step),
                                                                                         typeof(double),
                                                                                         typeof(DragNumberEditor),
                                                                                         new PropertyMetadata(0.1));

    public DragNumberEditor()
    {
        InitializeComponent();
    }

    public double MinValue
    {
        get => (double)GetValue(MinValueProperty);
        set => SetValue(MinValueProperty, value);
    }

    public double MaxValue
    {
        get => (double)GetValue(MaxValueProperty);
        set => SetValue(MaxValueProperty, value);
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public int DecimalPlaces
    {
        get => (int)GetValue(DecimalPlacesProperty);
        set => SetValue(DecimalPlacesProperty, value);
    }

    public double Step
    {
        get => (double)GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }
}
