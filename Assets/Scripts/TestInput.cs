using UnityEngine;

public class TestInput : MonoBehaviour
{
    // Source - https://stackoverflow.com/a/79024752
    // Posted by hijinxbassist
    // Retrieved 2025-12-01, License - CC BY-SA 4.0

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Space Pressed!");
        }
    }
}
