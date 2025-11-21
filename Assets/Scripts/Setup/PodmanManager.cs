using UnityEngine;
using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;

public class PodmanManager : MonoBehaviour
{
    public string PodmanInitPath;
    public string PodmanExePath;
    public string ContainerPath;

    private void Awake()
    {
        PodmanInitPath = System.IO.Path.Combine(Application.streamingAssetsPath.Replace("/", "\\"), "Go", "podman-init", "podman-init.exe");
        PodmanExePath = System.IO.Path.Combine(Application.streamingAssetsPath.Replace("/", "\\"), "RedHat", "Podman", "podman.exe");
        ContainerPath = System.IO.Path.Combine(Application.streamingAssetsPath.Replace("/", "\\"), "debian-argos.oci.tar");

        Debug.Log("[Unity Podman Initializer]: Setting up podman-init...");

        if (!IsPodmanAccessible())
        {
            return;
        }
        Debug.Log("[Unity Podman Initializer]: A.R.G.O.S. podman image is up!");

    }

    private bool IsPodmanAccessible()
    {
        Debug.Log($"[Unity Podman Initializer]: Podman executable path: {PodmanExePath}");
        Debug.Log($"[Unity Podman Initializer]: Container oci.tar path: {ContainerPath}");

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = PodmanInitPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            Arguments = $"\"{PodmanExePath}\" \"{ContainerPath}\""
        };

        Process proc = Process.Start(psi);
        if (proc == null)
        {
            Debug.LogError("[Unity Podman Initializer]: Failed to start process.");
            return false;
        }

        proc.WaitForExit();
        string stdout = proc.StandardOutput.ReadToEnd();
        string stderr = proc.StandardError.ReadToEnd();
        Debug.Log($"[podman-init]: {stdout + stderr}");
        if (proc.ExitCode != 0)
        {
            Debug.Log($"[podman-init]: {stderr}");
            return false;
        }

        return true;
    }
}
