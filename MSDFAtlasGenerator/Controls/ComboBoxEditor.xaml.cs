using System.Windows;
using MSDFAtlasGenerator.Contracts;
using IEnumerable = System.Collections.IEnumerable;

namespace MSDFAtlasGenerator.Controls;

public partial class ComboBoxEditor : Editor
{
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource),
                                                                                                typeof(IEnumerable),
                                                                                                typeof(ComboBoxEditor),
                                                                                                new PropertyMetadata(null, (a, _) => ((ComboBoxEditor)a).UpdateActualItemsSource()));

    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof(SelectedItem),
                                                                                                 typeof(object),
                                                                                                 typeof(ComboBoxEditor),
                                                                                                 new PropertyMetadata(null));

    public static readonly DependencyProperty IsEnumProperty = DependencyProperty.Register(nameof(IsEnum),
                                                                                           typeof(bool),
                                                                                           typeof(ComboBoxEditor),
                                                                                           new PropertyMetadata(false, (a, _) => ((ComboBoxEditor)a).UpdateActualItemsSource()));

    public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register(nameof(EnumType),
                                                                                             typeof(Type),
                                                                                             typeof(ComboBoxEditor),
                                                                                             new PropertyMetadata(null, (a, _) => ((ComboBoxEditor)a).UpdateActualItemsSource()));

    public static readonly DependencyProperty ActualItemsSourceProperty = DependencyProperty.Register(nameof(ActualItemsSource),
                                                                                                      typeof(IEnumerable),
                                                                                                      typeof(ComboBoxEditor),
                                                                                                      new PropertyMetadata(null));

    public ComboBoxEditor()
    {
        InitializeComponent();
    }

    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public bool IsEnum
    {
        get => (bool)GetValue(IsEnumProperty);
        set => SetValue(IsEnumProperty, value);
    }

    public Type EnumType
    {
        get => (Type)GetValue(EnumTypeProperty);
        set => SetValue(EnumTypeProperty, value);
    }

    public IEnumerable ActualItemsSource
    {
        get => (IEnumerable)GetValue(ActualItemsSourceProperty);
        private set => SetValue(ActualItemsSourceProperty, value);
    }

    private void UpdateActualItemsSource()
    {
        ActualItemsSource = IsEnum && EnumType != null ? Enum.GetValues(EnumType) : ItemsSource;
    }
}
