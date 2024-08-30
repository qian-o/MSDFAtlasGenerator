using System.Diagnostics;

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
                              Action<string?>? outputReceived = null,
                              Action<string?>? errorReceived = null)
    {
        Process process = GetCmd(fileName, arguments, outputReceived, errorReceived);

        process.Start();

        process.WaitForExit();

        process.Dispose();
    }

    public static async Task RunCmdAsync(string fileName,
                                         string? arguments = null,
                                         Action<string?>? outputReceived = null,
                                         Action<string?>? errorReceived = null)
    {
        Process process = GetCmd(fileName, arguments, outputReceived, errorReceived);

        process.Start();

        await process.WaitForExitAsync();

        process.Dispose();
    }

    private static Process GetCmd(string fileName,
                                  string? arguments = null,
                                  Action<string?>? outputReceived = null,
                                  Action<string?>? errorReceived = null)
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

        process.OutputDataReceived += (sender, e) => outputReceived?.Invoke(e.Data);
        process.ErrorDataReceived += (sender, e) => errorReceived?.Invoke(e.Data);

        return process;
    }
}
