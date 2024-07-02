using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSetttings : MonoBehaviour
{
    public void ToggleFullscreen()
    {
        //FullScreenMode.Windowed

        if (Screen.fullScreen == true) 
        {
            Screen.fullScreen = false;
            Debug.Log(string.Format("Game is now windowed"));
        }
        else if (Screen.fullScreen == false) 
        {
            Screen.fullScreen = true;
            Debug.Log(string.Format("Game is now fullscreen"));
        }
    }
}
