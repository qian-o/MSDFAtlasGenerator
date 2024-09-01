using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MSDFAtlasGenerator.Models;

public partial class OutputData : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Log> logs = [];

    public void AddLog(Log log)
    {
        Application.Current.Dispatcher.Invoke(() => Logs.Add(log));
    }

    public void ClearLog()
    {
        Application.Current.Dispatcher.Invoke(Logs.Clear);
    }
}
