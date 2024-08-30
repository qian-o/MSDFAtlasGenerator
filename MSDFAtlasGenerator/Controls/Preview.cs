using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MSDFAtlasGenerator.Models;

namespace MSDFAtlasGenerator.Controls;

public class Preview : Control
{
    public static readonly DependencyProperty PreviewDataProperty = DependencyProperty.Register(nameof(PreviewData),
                                                                                                typeof(PreviewData),
                                                                                                typeof(Preview),
                                                                                                new PropertyMetadata(null, (a, _) => ((Preview)a).UpdatePreview()));

    public PreviewData? PreviewData
    {
        get { return (PreviewData?)GetValue(PreviewDataProperty); }
        set { SetValue(PreviewDataProperty, value); }
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
        if (PreviewData == null)
        {
            previewImage = null;
        }
        else
        {
            if (previewImage is null || previewImage.PixelWidth != PreviewData.Width || previewImage.PixelHeight != PreviewData.Height)
            {
                previewImage = new WriteableBitmap(PreviewData.Width, PreviewData.Height, 96, 96, PixelFormats.Bgra32, null);
            }

            previewImage.WritePixels(new Int32Rect(0, 0, PreviewData.Width, PreviewData.Height), PreviewData.Bgra, PreviewData.Width * 4, 0);

            flipY = PreviewData.FlipY;

            InvalidateVisual();
        }
    }
}
