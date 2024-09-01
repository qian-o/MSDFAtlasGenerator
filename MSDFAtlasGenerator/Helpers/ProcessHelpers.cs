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

    public static void OpenUri(Uri uri)
    {
        Start("explorer.exe", uri.ToString());
    }

    public static void OpenFolder(string folderPath)
    {
        Start("explorer.exe", folderPath);
    }

    public static void RunCmd(string fileName,
                              string? arguments = null,
                              OutputData? outputData = null)
    {
        Process process = StartCmd(fileName, arguments, outputData);

        process.WaitForExit();

        process.Dispose();
    }

    public static async Task RunCmdAsync(string fileName,
                                         string? arguments = null,
                                         OutputData? outputData = null)
    {
        Process process = StartCmd(fileName, arguments, outputData);

        await process.WaitForExitAsync();

        process.Dispose();
    }

    private static Process StartCmd(string fileName,
                                    string? arguments = null,
                                    OutputData? outputData = null)
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
                outputData?.AddLog(new Log(LogType.Info, e.Data));
            }
        };
        process.ErrorDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                outputData?.AddLog(new Log(LogType.Error, e.Data));
            }
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        return process;
    }
}
