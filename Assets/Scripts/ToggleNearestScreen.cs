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
                RawImg.enabled = true;
                RawImg.transform.GetComponent<WebBrowserUIFull>().disableKeyboardInputs = false;
                var webui = RawImg.transform.GetComponent<WebBrowserUIFull>();
                //if (webui.keyboardAndMouseHandlerCoroutine == null)
                //{
                //    webui.keyboardAndMouseHandlerCoroutine = StartCoroutine(webui.KeyboardAndMouseHandler());
                //} else
                //{
                //    Debug.Log(webui.keyboardAndMouseHandlerCoroutine.ToString());
                //    Debug.Log("not gonna start bc there is already something, haha loser.");
                //}
                Cursor.lockState = CursorLockMode.None;
                Fpc.cameraCanMove = false;
                Fpc.playerCanMove = false;
            }
            else
            {
                Debug.Log("Page not loaded; cannot open!!!");
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            RawImg.enabled = false;
            RawImg.transform.GetComponent<WebBrowserUIFull>().disableKeyboardInputs = true;
            var webui = RawImg.transform.GetComponent<WebBrowserUIFull>();
            //StopCoroutine(webui.keyboardAndMouseHandlerCoroutine);
            Cursor.lockState = CursorLockMode.Locked;
            Fpc.cameraCanMove = true;
            Fpc.playerCanMove = true;
        }
    }

    //private void SetRawImage()
    //{
    //    Collider[] cols = Physics.OverlapSphere(gameObject.transform.position, DetectionRadius);
    //    float lowestDistance = Mathf.Infinity;
    //    foreach (Collider c in cols)
    //    {
    //        float thisDistance = Vector3.Distance(c.transform.position, gameObject.transform.position);
    //        RawImage rawImg = c.GetComponentInChildren<RawImage>();
    //        if (thisDistance < lowestDistance && rawImg)
    //        {
    //            RawImg = rawImg;
    //        }
    //    }
    //}
}
