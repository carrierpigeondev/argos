using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VoltstroStudios.UnityWebBrowser;

public class ToggleNearestScreen : MonoBehaviour
{
    public float DetectionRadius;
    public RawImage RawImg;
    public FirstPersonController Fpc;
    public EventSystem EventSystem;
    public WebBrowserUIFull browserUIFull;

    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (RawImg.transform.GetComponent<ScreenImageHandler>().PageLoaded)
            {
                HandleScreenEnable();
            }
            else
            {
                Debug.Log("Page not loaded; cannot open!!!");
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            HandleScreenDisable();
        }
    }

    private void HandleScreenEnable()
    {
        if (RawImg.enabled)
        {
            return;
        }

        RawImg.enabled = true;
        RawImg.transform.GetComponent<WebBrowserUIFull>().disableKeyboardInputs = false;
        Cursor.lockState = CursorLockMode.None;
        Fpc.cameraCanMove = false;
        Fpc.playerCanMove = false;
    }

    private void HandleScreenDisable()
    {
        if (!RawImg.enabled)
        {
            return;
        }

        RawImg.enabled = false;
        RawImg.transform.GetComponent<WebBrowserUIFull>().disableKeyboardInputs = true;
        Cursor.lockState = CursorLockMode.Locked;
        Fpc.cameraCanMove = true;
        Fpc.playerCanMove = true;
    }
}
