using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class that controlls main menu
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Loads first level
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene("Level28");
    }

    /// <summary>
    /// Loads test level 00
    /// </summary>
    public void PlayLevel00()
    {
        SceneManager.LoadScene("TestScene01");
    }

    /// <summary>
    /// Loads first level
    /// </summary>
    public void PlayLevel01()
    {
        SceneManager.LoadScene("Level28");
    }

    /// <summary>
    /// Loads test level 00
    /// </summary>
    public void PlayLevel02()
    {
        Debug.Log("NOT READY YET!");
        SceneManager.LoadScene("TestScene01");
    }

    /// <summary>
    /// Close application
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
