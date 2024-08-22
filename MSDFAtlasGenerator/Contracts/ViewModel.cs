using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MSDFAtlasGenerator.Contracts;

public abstract partial class ViewModel<TView> : ObservableRecipient where TView : View
{
    protected ViewModel(TView view)
    {
        View = view;

        Initialize();
    }

    public TView View { get; }

    protected virtual void Initialize()
    {
    }

    [RelayCommand]
    protected virtual void ViewLoaded()
    {
    }

    [RelayCommand]
    protected virtual void ViewUnloaded()
    {
    }
}
