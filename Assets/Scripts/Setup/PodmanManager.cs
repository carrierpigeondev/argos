using UnityEngine;
using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;

public class PodmanManager : MonoBehaviour
{
    public string PodmanInitPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Go", "podman-init", "podman-init.exe");

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
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = PodmanInitPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        Process proc = Process.Start(psi);
        if (proc == null)
        {
            Debug.LogError("Failed to start process.");
            return false;
        }

        string stdout = proc.StandardOutput.ReadToEnd();
        string stderr = proc.StandardError.ReadToEnd();
        proc.WaitForExit();
        if (proc.ExitCode != 0)
        {
            Debug.LogError(stderr);
            return false;
        }

        return true;
    }
}
