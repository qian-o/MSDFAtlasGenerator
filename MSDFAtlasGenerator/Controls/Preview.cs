using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MSDFAtlasGenerator.Controls;

public class Preview : Control
{
    public static readonly DependencyProperty RgbaProperty = DependencyProperty.Register(nameof(Rgba), typeof(byte[]), typeof(Preview), new PropertyMetadata(null));

    private WriteableBitmap? previewImage;

    public byte[] Rgba
    {
        get { return (byte[])GetValue(RgbaProperty); }
        set { SetValue(RgbaProperty, value); }
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        int width = (int)ActualWidth;
        int height = (int)ActualHeight;

        if (width < 1 || height < 1)
        {
            return;
        }

        if (previewImage is null || previewImage.PixelWidth != width || previewImage.PixelHeight != height)
        {
            previewImage = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
        }

        if (Rgba != null)
        {
            previewImage.WritePixels(new Int32Rect(0, 0, width, height), Rgba, width * 4, 0);
        }

        drawingContext.DrawImage(previewImage, new Rect(0, 0, width, height));
    }
}
