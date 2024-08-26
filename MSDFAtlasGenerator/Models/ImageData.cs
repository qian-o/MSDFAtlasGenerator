using CommunityToolkit.Mvvm.ComponentModel;

namespace MSDFAtlasGenerator.Models;

public partial class ImageData : ObservableObject
{
    public ImageData(int width, int height, byte[]? data)
    {
        Width = width;
        Height = height;
        Data = data;
    }

    [ObservableProperty]
    private int width;

    [ObservableProperty]
    private int height;

    [ObservableProperty]
    private byte[]? data;
}
