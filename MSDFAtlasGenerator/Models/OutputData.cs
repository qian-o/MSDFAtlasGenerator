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
        lock (Logs)
        {
            Application.Current.Dispatcher.InvokeAsync(() => Logs.Add(log));
        }
    }

    public void ClearLog()
    {
        lock (Logs)
        {
            Application.Current.Dispatcher.InvokeAsync(Logs.Clear);
        }
    }
}
