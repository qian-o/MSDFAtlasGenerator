using CommunityToolkit.Mvvm.ComponentModel;

namespace MSDFAtlasGenerator.Models;

public partial class ImageData : ObservableObject
{
    public ImageData(int width, int height, byte[]? bgra, bool flipY)
    {
        Width = width;
        Height = height;
        Bgra = bgra;
        FlipY = flipY;
    }

    [ObservableProperty]
    private int width;

    [ObservableProperty]
    private int height;

    [ObservableProperty]
    private byte[]? bgra;

    [ObservableProperty]
    private bool flipY;
}
