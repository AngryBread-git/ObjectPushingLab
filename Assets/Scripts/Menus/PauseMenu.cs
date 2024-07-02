using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool currentlyPaused;

    [SerializeField] private GameObject _pauseMenu;


    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame Pause
    void Update()
    {

    }

    public void TogglePause() 
    {
        if (currentlyPaused) { Resume(); }
        else { Pause(); }
    }

    private void Resume() 
    {
        Time.timeScale = 1;
        currentlyPaused = false;

        _pauseMenu.SetActive(false);

    }

    private void Pause()
    {
        if (Time.timeScale == 0) { return;}
        Time.timeScale = 0;
        currentlyPaused = true;

        _pauseMenu.SetActive(true);    
    }

    public void LoadScene(int sceneNr) 
    {
        SceneManager.LoadScene(sceneNr);
    }

    public void QuitGame()
    {
        Debug.Log(string.Format("QuitGame"));
        Application.Quit();
    }
}
