using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VoltstroStudios.UnityWebBrowser;
using VoltstroStudios.UnityWebBrowser.Core;

public class ScreenImageHandler : MonoBehaviour
{
    public WebBrowserUIFull browserUIFull;

    public RawImage rawImage;
    public Texture BootTexture;
    public Texture ErrorTexture;

    public PodmanManager Pm;

    private bool pageLoaded = false;

    private void Start()
    {
        browserUIFull = GetComponent<WebBrowserUIFull>();
        browserUIFull.browserClient.OnLoadFinish += OnLoadFinish;
    }

    private void OnLoadFinish(string url)
    {
        StartCoroutine(PageIsLoaded());
    }

    private IEnumerator PageIsLoaded()
    {
        yield return new WaitForSeconds(1f);
        while (browserUIFull.browserClient.BrowserTexture == null)
        {
            yield return null;
        }

        pageLoaded = true;
    }

    private void Update()
    {
        if (Pm.PodmanHasBeenChecked)
        {
            if (Pm.PodmanAccessible)
            {
                Debug.Log("[ScreenImageHandler]: Podman is accessible, showing browser texture.");
                rawImage.color = Color.black;
                browserUIFull.enabled = true;  // only enable here so that the browser doesn't try to load before podman is up, thus not needing manual refresh
                if (pageLoaded)
                {
                    rawImage.texture = browserUIFull.browserClient.BrowserTexture;
                    rawImage.color = Color.white;
                } else
                {
                    rawImage.texture = BootTexture;
                }
            } else
            {
                Debug.Log("[ScreenImageHandler]: Podman is not accessible, showing error texture.");
                rawImage.texture = ErrorTexture;
            }

        } else
        {
            Debug.Log("[ScreenImageHandler]: Podman has not been checked yet, showing boot texture.");
            rawImage.texture = BootTexture;
        }
    }
}
