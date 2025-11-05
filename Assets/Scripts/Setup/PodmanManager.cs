using UnityEngine;
using Process = System.Diagnostics.Process;
using ProcessStartInfo = System.Diagnostics.ProcessStartInfo;

public class PodmanManager : MonoBehaviour
{
    public string PodmanInitPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Go", "podman-init", "podman-init.exe");
    public string PodmanExePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RedHat", "Podman", "podman.exe");
    public string ContainerPath = System.IO.Path.Combine(Application.streamingAssetsPath, "debian-argos.oci.tar");

    private void Awake()
    {
        Debug.Log("Setting up podman-init...");

        if (!IsPodmanAccessible())
        {
            return;
        }
        Debug.Log("argos docker image is up!");

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
            Arguments = $"{PodmanExePath} {ContainerPath}"
        };

        Process proc = Process.Start(psi);
        if (proc == null)
        {
            Debug.LogError("Failed to start process.");
            return false;
        }

        
        proc.WaitForExit();
        string stdout = proc.StandardOutput.ReadToEnd();
        string stderr = proc.StandardError.ReadToEnd();
        Debug.Log(stdout + stderr);
        //if (proc.ExitCode != 0)
        //{
        //Debug.LogError(stderr);
        //return false;
        //}

        return true;
    }
}
