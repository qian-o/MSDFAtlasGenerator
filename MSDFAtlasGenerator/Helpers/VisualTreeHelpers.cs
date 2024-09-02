using System.Windows;
using System.Windows.Media;

namespace MSDFAtlasGenerator.Helpers;

public static class VisualTreeHelpers
{
    public static T? GetChildByType<T>(DependencyObject dependencyObject) where T : DependencyObject
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);

            if (child is T result)
            {
                return result;
            }

            if (GetChildByType<T>(child) is T childOfChild)
            {
                return childOfChild;
            }
        }

        return null;
    }
}
