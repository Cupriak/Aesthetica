using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private AttributesController playerAttributes;
    [SerializeField] private TMP_Text healthText;
    private string startHealthText;

    public static bool isGamePaused;

    [SerializeField] private Image pauseBackground;
    [SerializeField] private GameObject pauseSign;

    private void Awake()
    {
        isGamePaused = false;
        startHealthText = healthText.text;
        pauseBackground.enabled = false;
        pauseSign.SetActive(false);
    }

    private void Update ()
    {
        healthText.text = startHealthText + playerAttributes.Health;
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

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

    private void OnDestroy()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
