using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool currentlyPaused;

    [SerializeField] private GameObject _pauseMenuParent;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    private void Start()
    {
        //_pauseMenuParent.SetActive(false);
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

        _pauseMenuParent.SetActive(false);

    }

    private void Pause()
    {
        if (Time.timeScale == 0) { return;}
        Time.timeScale = 0;
        currentlyPaused = true;

        _pauseMenuParent.SetActive(true);
        _pauseMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }

    public void LoadScene(int sceneNr) 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNr);
    }

    public void QuitGame()
    {
        Debug.Log(string.Format("QuitGame"));
        Application.Quit();
    }
}
