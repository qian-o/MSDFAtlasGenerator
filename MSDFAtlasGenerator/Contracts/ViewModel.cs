using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MSDFAtlasGenerator.Contracts;

public abstract partial class ViewModel<TView>(TView view) : ObservableRecipient where TView : View
{
    public TView View { get; } = view;

    [RelayCommand]
    protected virtual void ViewLoaded()
    {
    }

    [RelayCommand]
    protected virtual void ViewUnloaded()
    {
    }
}
