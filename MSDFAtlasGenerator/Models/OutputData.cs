using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MSDFAtlasGenerator.Models;

public partial class OutputData : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Log> logs = [];

    public void AddLog(Log log)
    {
        Logs.Add(log);
    }

    public void ClearLog()
    {
        Logs.Clear();
    }
}
