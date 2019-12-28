using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Class that is controlling ingame UI
/// </summary>
public class UIController : MonoBehaviour
{
    /// <summary>
    /// Attributes of player that will be displayed
    /// </summary>
    [SerializeField] private AttributesController playerAttributes;
    /// <summary>
    /// Text that will display health
    /// </summary>
    [SerializeField] private TMP_Text healthText;
    /// <summary>
    /// String that will be displayed before number of current HP
    /// </summary>
    private string startHealthText;

    /// <summary>
    /// Flag that check if the game is paused
    /// </summary>
    public static bool isGamePaused;

    /// <summary>
    /// Background image that will be display when game is paused
    /// </summary>
    [SerializeField] private Image pauseBackground;
    /// <summary>
    /// Pause sign that will be display when game is paused
    /// </summary>
    [SerializeField] private GameObject pauseSign;

    /// <summary>
    /// Call on object creation. Set initial values.
    /// </summary>
    private void Awake()
    {
        isGamePaused = false;
        startHealthText = healthText.text;
        pauseBackground.enabled = false;
        pauseSign.SetActive(false);
    }

    /// <summary>
    /// Called every frame.
    /// Updates health value
    /// </summary>
    private void Update ()
    {
        healthText.text = startHealthText + playerAttributes.Health;
    }

    /// <summary>
    /// Loads scene with main menu
    /// </summary>
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Pause or Unpause the game depends of current game state
    /// </summary>
    public void Pause()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1f;
            isGamePaused = false;
            FindObjectOfType<AudioManager>().ChangeVolume("LevelTheme", 1f);
            pauseBackground.enabled = false;
            pauseSign.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            isGamePaused = true;
            FindObjectOfType<AudioManager>().ChangeVolume("LevelTheme", 0.5f);
            pauseBackground.enabled = true;
            pauseSign.SetActive(true);
        }
    }

    /// <summary>
    /// Called when object is destroyed. 
    /// By default when scene is destroyed.
    /// Turn off pause and set time to default value
    /// </summary>
    private void OnDestroy()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    /// <summary>
    /// Closes application
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
