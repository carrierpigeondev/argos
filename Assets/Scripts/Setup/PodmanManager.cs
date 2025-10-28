using UnityEngine;
using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;


public class PodmanManager : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Setting up Podman...");

        if (!IsPodmanAccessible())
        {
            return;
        }
        Debug.Log("Podman is accessible and ready to use!");
    }

    private bool IsPodmanAccessible()
    {
        Process proc = Process.Start(new ProcessStartInfo
        {
            FileName = System.IO.Path.Combine(
                Application.streamingAssetsPath,
                "Redhat",
                "Podman",
                "podman.exe"
            ),
            Arguments = "--version",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        });

        if (proc == null)
        {
            Debug.LogError("Failed to start Podman process.");
            return false;
        }

        string stdout = proc.StandardOutput.ReadToEnd();
        string stderr = proc.StandardError.ReadToEnd();
        proc.WaitForExit();

        Debug.Log("Podman Output: " + stdout);
        if (proc.ExitCode != 0)
        {
            Debug.LogError("Podman Error: " + stderr);
            return false;
        }

        return true;
    }
}
