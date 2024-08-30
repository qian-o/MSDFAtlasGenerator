using CommunityToolkit.Mvvm.ComponentModel;
using MSDFAtlasGenerator.Enums;

namespace MSDFAtlasGenerator.Models;

public partial class Log : ObservableObject
{
    public Log(LogType type, string message)
    {
        Type = type;
        Message = message;
    }

    [ObservableProperty]
    private LogType type;

    [ObservableProperty]
    private string message;
}
