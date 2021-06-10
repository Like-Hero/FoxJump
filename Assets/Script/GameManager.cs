using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private KeyCode ExitKey;

    public Animator PauseMenuAnim;

    private static GameManager _ins;

    public bool isPause;
    public bool pauseMenuIsReady;

    public static GameManager Ins { get { return _ins; } }
    private void Start()
    {
        if(_ins == null)
        {
            _ins = this;
        }
        DontDestroyOnLoad(gameObject);
        ExitKey = KeyCode.Escape;
        pauseMenuIsReady = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(ExitKey) && pauseMenuIsReady)
        {
            if (isPause)
            {
                OnResume();
            }
            else
            {
                OnPause();
            }
        }
    }
    private void OnResume()
    {
        isPause = false;
        pauseMenuIsReady = false;
        PauseMenuAnim.SetBool("pause", false);
    }
    private void OnPause()
    {
        isPause = true;
        pauseMenuIsReady = false;
        PauseMenuAnim.SetBool("pause", true);
    }
    
}
