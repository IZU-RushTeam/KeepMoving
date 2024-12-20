using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool MusicIsMuted = false;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] GameObject pauseMenu;
    public Sprite soundOnImage;
    public Sprite soundOffImage;
    public Button button;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Home()
    {
        buttonstriggered();
        SceneManager.LoadScene("Main Menu");
    }
    public void Resume()
    {
        buttonstriggered();
    }
    public void Restart()
    {
        buttonstriggered();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void buttonstriggered()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Volume()
    {
        if (!MusicIsMuted)
        {
            button.image.sprite = soundOffImage;
            audioMixer.SetFloat("Master", -80);
            MusicIsMuted = true;

        }
        else
        {
            button.image.sprite = soundOnImage;
            audioMixer.SetFloat("Master", 0);
            MusicIsMuted = false;
        }
    }
}
