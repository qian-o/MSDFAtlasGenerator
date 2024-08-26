using CommunityToolkit.Mvvm.ComponentModel;

namespace MSDFAtlasGenerator.Models;

public partial class Metrics : ObservableObject
{
    [ObservableProperty]
    private double emSize;

    [ObservableProperty]
    private double lineHeight;

    [ObservableProperty]
    private double ascender;

    [ObservableProperty]
    private double descender;

    [ObservableProperty]
    private double underlineY;

    [ObservableProperty]
    private double underlineThickness;
}
