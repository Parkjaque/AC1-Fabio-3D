using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    public void btnPlay_click()
    {
        SceneManager.LoadScene("game");
    }

    public void btnSettings_click()
    {
        settingsMenu.SetActive(true);
    }

    public void btnQuit_click()
    {
        Application.Quit();
    }
}
