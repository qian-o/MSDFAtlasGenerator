using MSDFAtlasGenerator.Contracts;
using MSDFAtlasGenerator.ViewModels;

namespace MSDFAtlasGenerator.Views;

public partial class GeneratorPage : View
{
    public GeneratorPage()
    {
        InitializeComponent();

        DataContext = new GeneratorViewModel(this);
    }
}
