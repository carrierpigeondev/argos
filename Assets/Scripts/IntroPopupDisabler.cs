using UnityEngine;

public class IntroPopupDisabler : MonoBehaviour
{
    public Canvas IntroPopupCanvas;

    private bool didDisable;

    private void Update()
    {
        if (!didDisable)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                IntroPopupCanvas.enabled = false;
                didDisable = true;
            }
        }
    }
}
