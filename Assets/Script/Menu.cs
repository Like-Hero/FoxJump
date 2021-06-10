using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject MainUI;
    public AudioMixer audioMixer;
    public Animator pauseMenuAnim;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ShowUI()
    {
        MainUI.SetActive(true);
    }
    public void PauseMenuIsReady()
    {
        GameManager.Ins.pauseMenuIsReady = true;
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    public void Resume()
    {
        GameManager.Ins.isPause = false;
        GameManager.Ins.pauseMenuIsReady = true;
        pauseMenuAnim.SetBool("pause", false);
    }
    public void Pause()
    {
        GameManager.Ins.isPause = true;
        GameManager.Ins.pauseMenuIsReady = true;
        pauseMenuAnim.SetBool("pause", true);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
    }
}
