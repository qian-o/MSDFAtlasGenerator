using System.Diagnostics;
using MSDFAtlasGenerator.Enums;
using MSDFAtlasGenerator.Models;

namespace MSDFAtlasGenerator.Helpers;

public static class ProcessHelpers
{
    public static void Start(string fileName,
                             string? arguments = null)
    {
        Process process = new()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments
            }
        };

        process.Start();
    }

    public static void RunCmd(string fileName,
                              string? arguments = null,
                              Action<Log>? outputReceived = null,
                              Action<Log>? errorReceived = null)
    {
        Process process = StartCmd(fileName, arguments, outputReceived, errorReceived);

        process.WaitForExit();

        process.Dispose();
    }

    public static async Task RunCmdAsync(string fileName,
                                         string? arguments = null,
                                         Action<Log>? outputReceived = null,
                                         Action<Log>? errorReceived = null)
    {
        Process process = StartCmd(fileName, arguments, outputReceived, errorReceived);

        await process.WaitForExitAsync();

        process.Dispose();
    }

    private static Process StartCmd(string fileName,
                                    string? arguments = null,
                                    Action<Log>? outputReceived = null,
                                    Action<Log>? errorReceived = null)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = fileName,
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        Process process = new()
        {
            StartInfo = startInfo
        };

        process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                outputReceived?.Invoke(new Log(LogType.Info, e.Data));
            }
        };
        process.ErrorDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                errorReceived?.Invoke(new Log(LogType.Error, e.Data));
            }
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        return process;
    }
}
