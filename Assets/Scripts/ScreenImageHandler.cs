using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VoltstroStudios.UnityWebBrowser;
using VoltstroStudios.UnityWebBrowser.Core;

public class ScreenImageHandler : MonoBehaviour
{
    public WebBrowserUIFull BrowserUIFull;

    public RawImage rawImage;
    public Texture BootTexture;
    public Texture ErrorTexture;

    public EventSystem EventSystem;

    public PodmanManager Pm;

    public bool PageLoaded = false;

    public bool setTexture = false;

    public GameObject DisplayObject;



    private void Start()
    {
        BrowserUIFull = GetComponent<WebBrowserUIFull>();
        BrowserUIFull.browserClient.OnLoadFinish += OnLoadFinish;
    }

    private void OnLoadFinish(string url)
    {
        StartCoroutine(PageIsLoaded());
    }

    private IEnumerator PageIsLoaded()
    {
        yield return new WaitForSeconds(1f);
        while (BrowserUIFull.browserClient.BrowserTexture == null)
        {
            yield return null;
        }

        PageLoaded = true;

        //Debug.Log("INFO INFO INFO INFO INFO; FAKING EVENT");
        //PointerEventData fakeData = new(EventSystem);
        //BrowserUIFull.OnPointerEnter(fakeData);  // fake a call OnPointerEnter so that the BrowserUI (RawImageUwbClientInputHandler) recognizes keyboard inputs 
    }

    private void Update()
    {
        UpdateDisplayObjectTexture();

        if (Pm.PodmanHasBeenChecked)
        {
            if (Pm.PodmanAccessible)
            {
                //Debug.Log("[ScreenImageHandler]: Podman is accessible, showing browser texture.");
                BrowserUIFull.enabled = true;  // only enable here so that the browser doesn't try to load before podman is up, thus not needing manual refresh
                if (PageLoaded)
                {
                    if (!setTexture)
                    {
                        setTexture = true;
                        rawImage.texture = BrowserUIFull.browserClient.BrowserTexture;
                        if (DisplayObject.GetComponent<Transform>().localRotation != Quaternion.identity)
                        {
                            DisplayObject.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
                            DisplayObject.GetComponent<Transform>().localScale = new Vector3(-0.8f, 0.45f, 0.01f);
                        }
                        rawImage.color = Color.white;
                    }
                    
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

    private void UpdateDisplayObjectTexture()
    {
        DisplayObject.GetComponent<Renderer>().material.mainTexture = rawImage.texture;
    }
}
