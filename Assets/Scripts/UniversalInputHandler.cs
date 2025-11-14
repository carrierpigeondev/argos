using System;
using System.Collections.Generic;
using UnityEngine;
using VoltstroStudios.UnityWebBrowser.Input;
using VoltstroStudios.UnityWebBrowser.Shared;

namespace VoltstroStudios.UnityWebBrowser.Input
{
    public class UniversalInputHandler : MonoBehaviour
    {
        private static readonly KeyCode[] Keymap = (KeyCode[])Enum.GetValues(typeof(KeyCode));
        public WebBrowserInputHandler inputHandler;

        public WindowsKey[] GetDownKeys()
        {
            List<WindowsKey> keysDown = new();

            foreach (KeyCode key in Keymap)
            {
                //Why are mouse buttons considered key codes???
                if (key is KeyCode.Mouse0 or KeyCode.Mouse1 or KeyCode.Mouse2 or KeyCode.Mouse3 or KeyCode.Mouse4
                    or KeyCode.Mouse5 or KeyCode.Mouse6)
                    continue;

                try
                {
                    if (UnityEngine.Input.GetKeyDown(key))
                    {
                        Debug.Log("DOWN" + key.ToString());
                        keysDown.Add((WindowsKey)key);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    //Safe to ignore
                }
            }

            Debug.Log(keysDown.ToArray());
            return keysDown.ToArray();
        }

        public WindowsKey[] GetUpKeys()
        {
            List<WindowsKey> keysUp = new();

            foreach (KeyCode key in Keymap)
            {
                //Why are mouse buttons considered key codes???
                if (key is KeyCode.Mouse0 or KeyCode.Mouse1 or KeyCode.Mouse2 or KeyCode.Mouse3 or KeyCode.Mouse4
                    or KeyCode.Mouse5 or KeyCode.Mouse6)
                    continue;

                try
                {
                    if (UnityEngine.Input.GetKeyUp(key))
                    {
                        Debug.Log("UP" + key.ToString());
                        keysUp.Add((WindowsKey)key);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    //Safe to ignore
                }
            }

            Debug.Log(keysUp.ToArray());
            return keysUp.ToArray();
        }
    }
}
