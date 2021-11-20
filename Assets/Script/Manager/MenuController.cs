using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuController : Singleton<MenuController>
{
    public float animationDeltaTimeRate;
    private AudioSource audioSource;
    private float menuOriginPosY;
    public bool isPause { get; set; }
    public bool pauseMenuIsReady { get; set; }
    private Vector3 m_menuOriginPos;
    private Vector3 m_menuPausePos;
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        menuOriginPosY = GetComponent<RectTransform>().anchoredPosition3D.y;
        pauseMenuIsReady = true;
        m_menuOriginPos = GetComponent<RectTransform>().anchoredPosition3D;
        m_menuPausePos = Vector3.zero;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuIsReady)
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
    public void OnResume()
    {
        isPause = false;
        pauseMenuIsReady = false;
        StartCoroutine(Resume());  
    }
    public void OnPause()
    {
        isPause = true;
        pauseMenuIsReady = false;
        StartCoroutine(Pause());
    }
    private IEnumerator Pause()
    {
        Time.timeScale = 0f;
        audioSource.Play();
        RectTransform rectTransform = GetComponent<RectTransform>();//需要移动的UI的RectTransform
        float lastFrameTime;//上一帧的时间
        float currentFrameTime;//当前帧的时间
        float deltaTime;//相邻两帧的时间间隔
        while (rectTransform.anchoredPosition3D.y > Mathf.Epsilon)//如果UI的y坐标大于0（Mathf.Epsilon代表浮点数的最小值，浮点数运算尽量别用整数）
        {
            lastFrameTime = Time.realtimeSinceStartup;//更新上一帧的时间
            yield return null;//跳过这一帧，为了在下一帧计算相邻两帧的间隔
            currentFrameTime = Time.realtimeSinceStartup;//更新当前帧的时间
            deltaTime = currentFrameTime - lastFrameTime;//计算相邻两帧的时间间隔
            /*
             * menuOriginPosY代表UI的原来的y坐标
             * rectTransform.anchoredPosition3D + Vector3.down * menuOriginPosY的意思是将UI的y坐标从原来的位置变换到y=0的位置，也就是中心位置
             */
            rectTransform.anchoredPosition3D = Vector3.Lerp(
                rectTransform.anchoredPosition3D,
                rectTransform.anchoredPosition3D + Vector3.down * menuOriginPosY,//可以理解为 y = y + (-1) * y;
                deltaTime * animationDeltaTimeRate);//animationDeltaTimeRate是变换频率，如果该值过高变换速度快
        }
        //指定到暂停的位置，因为Lerp不能准确定位到一个整数，所以我们这里给它定位一下
        rectTransform.anchoredPosition3D = m_menuPausePos;
        pauseMenuIsReady = true;
    }
    public IEnumerator Resume()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float lastFrameTime;
        float currentFrameTime;
        float deltaTime;
        while (rectTransform.anchoredPosition3D.y < menuOriginPosY)
        {
            lastFrameTime = Time.realtimeSinceStartup;
            yield return null;
            currentFrameTime = Time.realtimeSinceStartup;
            deltaTime = currentFrameTime - lastFrameTime;
            rectTransform.anchoredPosition3D = Vector3.Lerp(
                rectTransform.anchoredPosition3D, 
                rectTransform.anchoredPosition3D + Vector3.up * menuOriginPosY,
                deltaTime * animationDeltaTimeRate);
        }
        //恢复到原位
        rectTransform.anchoredPosition3D = m_menuOriginPos;
        pauseMenuIsReady = true;
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneController.Ins.TransitionSameSceneEnter();
        //gameObject.SetActive(false);
    }
    public void LoadMain()
    {
        Time.timeScale = 1f;
        SceneController.Ins.LoadMain();
        //gameObject.SetActive(false);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
