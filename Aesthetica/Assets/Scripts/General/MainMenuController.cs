using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level28");
    }

    public void PlayLevel00()
    {
        SceneManager.LoadScene("TestScene01");
    }

    public void PlayLevel01()
    {
        SceneManager.LoadScene("Level28");
    }

    public void PlayLevel02()
    {
        Debug.Log("NOT READY YET!");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
