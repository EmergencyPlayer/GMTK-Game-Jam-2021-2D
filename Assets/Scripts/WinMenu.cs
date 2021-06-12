using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public bool winGame;

    public GameObject winMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (winGame)
        {
            winGamePause();
        }
        else
        {
            winGameResume();
        }
    }

    public void winGameResume()
    {
        winMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void winGamePause()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void nextScene()
    {
        winMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
