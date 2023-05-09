using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(540, 960, false);
    }

    private void Update()
    {
        Screen.SetResolution(Screen.width, ((Screen.width * 16) / 9), false); 
        //if (Screen.fullScreenMode != FullScreenMode.Windowed)
        //{
        //    Screen.fullScreenMode = FullScreenMode.Windowed;
        //}
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameLoopScene");
    }

    public void ExitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
