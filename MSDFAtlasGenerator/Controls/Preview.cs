using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MSDFAtlasGenerator.Models;

namespace MSDFAtlasGenerator.Controls;

public class Preview : Control
{
    public static readonly DependencyProperty ImageDataProperty = DependencyProperty.Register(nameof(ImageData),
                                                                                              typeof(ImageData),
                                                                                              typeof(Preview),
                                                                                              new PropertyMetadata(null, (a, _) => ((Preview)a).UpdatePreview()));

    public ImageData ImageData
    {
        get { return (ImageData)GetValue(ImageDataProperty); }
        set { SetValue(ImageDataProperty, value); }
    }

    private WriteableBitmap? previewImage;
    private bool flipY;

    protected override void OnRender(DrawingContext drawingContext)
    {
        if (previewImage == null)
        {
            return;
        }

        double width = ActualWidth;
        double height = ActualHeight;

        ImageBrush imageBrush = new(previewImage)
        {
            AlignmentX = AlignmentX.Center,
            AlignmentY = AlignmentY.Center,
            Stretch = Stretch.Uniform
        };

        if (flipY)
        {
            drawingContext.PushTransform(new TranslateTransform(0.0, height));
            drawingContext.PushTransform(new ScaleTransform(1.0, -1.0));

            drawingContext.DrawRectangle(imageBrush, null, new Rect(0.0, 0.0, width, height));

            drawingContext.Pop();
            drawingContext.Pop();
        }
        else
        {
            drawingContext.DrawRectangle(imageBrush, null, new Rect(0.0, 0.0, width, height));
        }
    }

    private void UpdatePreview()
    {
        if (ImageData == null)
        {
            previewImage = null;
        }
        else
        {
            if (previewImage is null || previewImage.PixelWidth != ImageData.Width || previewImage.PixelHeight != ImageData.Height)
            {
                previewImage = new WriteableBitmap(ImageData.Width, ImageData.Height, 96, 96, PixelFormats.Bgra32, null);
            }

            previewImage.WritePixels(new Int32Rect(0, 0, ImageData.Width, ImageData.Height), ImageData.Bgra, ImageData.Width * 4, 0);

            flipY = ImageData.FlipY;

            InvalidateVisual();
        }
    }
}
