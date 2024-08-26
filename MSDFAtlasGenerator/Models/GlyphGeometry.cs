using CommunityToolkit.Mvvm.ComponentModel;

namespace MSDFAtlasGenerator.Models;

public partial class GlyphGeometry : ObservableObject
{
    [ObservableProperty]
    private int unicode;

    [ObservableProperty]
    private double advance;

    [ObservableProperty]
    private Bounds planeBounds;

    [ObservableProperty]
    private Bounds atlasBounds;
}
