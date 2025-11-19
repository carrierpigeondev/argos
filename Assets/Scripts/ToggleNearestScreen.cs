using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleNearestScreen : MonoBehaviour
{
    public float DetectionRadius;
    public RawImage RawImg;
    public FirstPersonController Fpc;

    void Update()
    {
        SetRawImage();
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (RawImg)
            {
                RawImg.enabled = true;
                Cursor.lockState = CursorLockMode.None;
                Fpc.cameraCanMove = false;
                Fpc.playerCanMove = false;
            }
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Insert))
        {
            if (RawImg)
            {
                RawImg.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Fpc.cameraCanMove = true;
                Fpc.playerCanMove = true;
            }
        }
    }

    private void SetRawImage()
    {
        Collider[] cols = Physics.OverlapSphere(gameObject.transform.position, DetectionRadius);
        float lowestDistance = Mathf.Infinity;
        foreach (Collider c in cols)
        {
            float thisDistance = Vector3.Distance(c.transform.position, gameObject.transform.position);
            RawImage rawImg = c.GetComponentInChildren<RawImage>();
            if (thisDistance < lowestDistance && rawImg)
            {
                RawImg = rawImg;
            }
        }
    }
}
