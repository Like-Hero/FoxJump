using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private KeyCode ExitKey;

    public Animator PauseMenuAnim;

    public bool isPause;
    public bool pauseMenuIsReady;

    public Text cherryValue;//樱桃Text
    public Text gemValue;//宝石Text

    private static GameManager _ins;
    public static GameManager Ins { get { return _ins; } }
    private void Start()
    {
        if(_ins == null)
        {
            _ins = this;
        }
        //DontDestroyOnLoad(gameObject);
        ExitKey = KeyCode.Escape;
        pauseMenuIsReady = true;
    }
    private void Update()
    {
        UpdateCollectionValue();
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
    private void UpdateCollectionValue()
    {
        cherryValue.text = PlayerData.CheeryCount.ToString();
        gemValue.text = PlayerData.GemCount.ToString();
    }
}
