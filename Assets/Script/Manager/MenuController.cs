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
        RectTransform rectTransform = GetComponent<RectTransform>();//��Ҫ�ƶ���UI��RectTransform
        float lastFrameTime;//��һ֡��ʱ��
        float currentFrameTime;//��ǰ֡��ʱ��
        float deltaTime;//������֡��ʱ����
        while (rectTransform.anchoredPosition3D.y > Mathf.Epsilon)//���UI��y�������0��Mathf.Epsilon������������Сֵ�����������㾡������������
        {
            lastFrameTime = Time.realtimeSinceStartup;//������һ֡��ʱ��
            yield return null;//������һ֡��Ϊ������һ֡����������֡�ļ��
            currentFrameTime = Time.realtimeSinceStartup;//���µ�ǰ֡��ʱ��
            deltaTime = currentFrameTime - lastFrameTime;//����������֡��ʱ����
            /*
             * menuOriginPosY����UI��ԭ����y����
             * rectTransform.anchoredPosition3D + Vector3.down * menuOriginPosY����˼�ǽ�UI��y�����ԭ����λ�ñ任��y=0��λ�ã�Ҳ��������λ��
             */
            rectTransform.anchoredPosition3D = Vector3.Lerp(
                rectTransform.anchoredPosition3D,
                rectTransform.anchoredPosition3D + Vector3.down * menuOriginPosY,//�������Ϊ y = y + (-1) * y;
                deltaTime * animationDeltaTimeRate);//animationDeltaTimeRate�Ǳ任Ƶ�ʣ������ֵ���߱任�ٶȿ�
        }
        //ָ������ͣ��λ�ã���ΪLerp����׼ȷ��λ��һ�������������������������λһ��
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
        //�ָ���ԭλ
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
