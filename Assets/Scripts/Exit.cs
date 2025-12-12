using UnityEditor;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Application.Quit();
        }
    }
}
