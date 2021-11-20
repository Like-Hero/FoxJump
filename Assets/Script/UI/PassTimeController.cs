using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassTimeController : Singleton<PassTimeController>
{
    private Text m_timeText;
    protected override void Awake()
    {
        base.Awake();
        m_timeText = GetComponent<Text>();
    }
    private void Start()
    {
        StartCoroutine(Timer());
    }
    private void Update()
    {
        m_timeText.text = TimeUtility.TimeFormat((int)GameManager.Ins.currentPassData.accessTime);
    }
    private IEnumerator Timer()
    {
        while (true)
        {
            GameManager.Ins.currentPassData.accessTime += Time.deltaTime;
            yield return null;
        }
    }
    
}
