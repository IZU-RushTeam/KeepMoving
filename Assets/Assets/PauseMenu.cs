using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool MusicIsMuted = false;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] GameObject pauseMenu;
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
        if (MusicIsMuted!)
        {
            audioMixer.SetFloat("Master", -80f);
            MusicIsMuted = true;
        }
        else
        {
            audioMixer.SetFloat("Master", 0f);
            MusicIsMuted = false;
        }
    }
}
