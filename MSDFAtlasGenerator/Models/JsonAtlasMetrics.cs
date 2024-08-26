using CommunityToolkit.Mvvm.ComponentModel;

namespace MSDFAtlasGenerator.Models;

public partial class JsonAtlasMetrics : ObservableObject
{
    public JsonAtlasMetrics(Atlas atlas, Metrics metrics, GlyphGeometry[] glyphs)
    {
        Atlas = atlas;
        Metrics = metrics;
        Glyphs = glyphs;
    }

    [ObservableProperty]
    private Atlas atlas;

    [ObservableProperty]
    private Metrics metrics;

    [ObservableProperty]
    private GlyphGeometry[] glyphs;
}
