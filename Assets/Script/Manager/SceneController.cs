using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : Singleton<SceneController>
{
    public SceneFader sceneFader;
    private Coroutine fadeCoroutine;
    public GameObject TransitionButton;
    private GameObject m_TransitionButton;
    public PlayerController PlayerPrefab;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void LoadMain()
    {
        StartCoroutine(LoadMainIE());
    }
    public IEnumerator LoadMainIE()
    {
        if (fadeCoroutine != null)
            yield break;
        SceneFader fader = Instantiate(sceneFader);
        //先渐入
        yield return fadeCoroutine = StartCoroutine(fader.FadeOut(fader.differentSceneFadeOutDuration));
        yield return SceneManager.LoadSceneAsync(0);
        yield return StartCoroutine(fader.FadeIn(fader.sameSceneFadeInDuration));
        fadeCoroutine = null;
    }
    public void TransitionLevel(int level)
    {
        StartCoroutine(Transtion(level, TransitionDestination.DestinationTag.Enter));
    }
    public void TransitionNextScene()
    {
        StartCoroutine(Transtion(SceneManager.GetActiveScene().buildIndex + 1, TransitionDestination.DestinationTag.Enter));
    }
    public void TransitionSameSceneEnter()
    {
        StartCoroutine(Transtion(SceneManager.GetActiveScene().buildIndex, TransitionDestination.DestinationTag.Enter));
    }
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch (transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transtion(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:
                StartCoroutine(Transtion(transitionPoint.sceneName, transitionPoint.destinationTag));
                break;
        }
    }

    internal void DestoryTransitionButton()
    {
        if(m_TransitionButton != null)
        {
            Destroy(m_TransitionButton.gameObject);
        }
    }

    internal void InstantiateTransitionButton(Transform transitionTransform)
    {
        if(m_TransitionButton == null)
        {
            m_TransitionButton = Instantiate(TransitionButton, transitionTransform.position, transitionTransform.rotation, GameObject.Find("Canvas").transform);
            m_TransitionButton.transform.SetSiblingIndex(0);
            m_TransitionButton.GetComponent<UIFollowWorldObject>().Init(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(), transitionTransform, GameObject.Find("Canvas").GetComponent<Canvas>());
        }
    }
    private IEnumerator Transtion(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        if (fadeCoroutine != null)
            yield break;
        SceneFader fader = Instantiate(sceneFader);
        if (sceneName != SceneManager.GetActiveScene().name)
        {
            //先渐入
            yield return fadeCoroutine = StartCoroutine(fader.FadeOut(fader.differentSceneFadeOutDuration));
            //StartCoroutine(DelayFade(fader, false));
            //进入下一个场景
            //fadeCoroutine = null;
            yield return SceneManager.LoadSceneAsync(sceneName);
            
            yield return GameManager.Ins.Player = Instantiate(PlayerPrefab);
            if(GameManager.Ins.Player != null)
            {
                GameManager.Ins.Player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            }
            //渐出
            fader.gameObject.SetActive(true);
            
            yield return StartCoroutine(fader.FadeIn(fader.differentSceneFadeInDuration));
        }
        else
        {
            //先渐入
            yield return fadeCoroutine = StartCoroutine(fader.FadeOut(fader.sameSceneFadeOutDuration));
            //StartCoroutine(DelayFade(fader, false));
            //传送到指定位置
            TransitionDestination transitionDestination = GetDestination(destinationTag);
            GameManager.Ins.Player.transform.SetPositionAndRotation(
                transitionDestination.transform.position,
                transitionDestination.transform.rotation);
            //渐出
            //StartCoroutine(DelayFade(fader, true));
            yield return StartCoroutine(fader.FadeIn(fader.sameSceneFadeInDuration));
        }
        fadeCoroutine = null;
        yield return null;
    }
    private IEnumerator Transtion(int sceneIndex, TransitionDestination.DestinationTag destinationTag)
    {
        if (fadeCoroutine != null)
            yield break;
        SceneFader fader = Instantiate(sceneFader);
        //先渐入
        yield return fadeCoroutine = StartCoroutine(fader.FadeOut(fader.differentSceneFadeOutDuration));
        //StartCoroutine(DelayFade(fader, false));
        //进入下一个场景
        //fadeCoroutine = null;
        yield return SceneManager.LoadSceneAsync(sceneIndex);
        yield return GameManager.Ins.Player = Instantiate(PlayerPrefab);
        if (GameManager.Ins.Player != null)
        {
            GameManager.Ins.Player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
        }
        //渐出
        fader.gameObject.SetActive(true);
        yield return StartCoroutine(fader.FadeIn(fader.differentSceneFadeInDuration));
        fadeCoroutine = null;
        yield return null;
    }
    public TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
                return entrances[i];
        }
        return null;
    }
    private IEnumerator DelayFade(SceneFader fader, bool active)
    {
        yield return new WaitForSeconds(0.1f);
        fader.gameObject.SetActive(active);
    }
}
