using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public void LoadMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
