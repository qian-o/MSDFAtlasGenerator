using System.Diagnostics;

namespace MSDFAtlasGenerator.Helpers;

public static class ProcessHelpers
{
    public static void Run(string fileName,
                           string? arguments = null,
                           Action<string?>? outputReceived = null,
                           Action<string?>? errorReceived = null)
    {
        Process process = GetProcess(fileName, arguments, outputReceived, errorReceived);

        process.Start();

        process.WaitForExit();

        process.Dispose();
    }

    public static async Task RunAsync(string fileName,
                                      string? arguments = null,
                                      Action<string?>? outputReceived = null,
                                      Action<string?>? errorReceived = null)
    {
        Process process = GetProcess(fileName, arguments, outputReceived, errorReceived);

        process.Start();

        await process.WaitForExitAsync();

        process.Dispose();
    }

    private static Process GetProcess(string fileName,
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
