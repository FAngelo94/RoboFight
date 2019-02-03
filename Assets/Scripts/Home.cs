using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public void GoToSettings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }

    public void GoToPlay()
    {
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
