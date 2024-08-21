using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.ViewModels;

namespace MSDFAtlasGenerator.Views;

/// <summary>
/// GeneratorPage.xaml 的交互逻辑
/// </summary>
public partial class GeneratorPage : View
{
    public GeneratorPage()
    {
        InitializeComponent();

        DataContext = new GeneratorViewModel(this);
    }
}
