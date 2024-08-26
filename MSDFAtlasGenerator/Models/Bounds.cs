namespace MSDFAtlasGenerator.Models;

public record struct Bounds
{
    public double Left { get; set; }

    public double Bottom { get; set; }

    public double Right { get; set; }

    public double Top { get; set; }

    public readonly double Width => Right - Left;

    public readonly double Height => Top - Bottom;
}
