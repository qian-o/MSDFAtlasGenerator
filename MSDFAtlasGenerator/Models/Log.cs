using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Enums;

namespace MSDFAtlasGenerator.Models;

public partial class Log : ObservableObject
{
    public Log(LogType type, string message, bool appendTime = true)
    {
        Type = type;
        Message = appendTime ? $"[{DateTime.Now:HH:mm:ss}] {message}" : message;
    }

    [ObservableProperty]
    private LogType type;

    [ObservableProperty]
    private string message;
}
