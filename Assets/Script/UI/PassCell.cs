using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassCell : MonoBehaviour
{
    public int level;
    public Image icon;
    public Text levelText;
    public GameObject active;
    public TaskPanelController taskDescriptionPanel;
    private PassData m_passData;
    private void Start()
    {
        if (DataManager.IsInitialized)
        {
            //�Ȳ��Ҹùؿ��Ƿ񱻿������������͹ر�active
            m_passData = DataManager.Ins.PassDatas.passDatas[level - 1];
            icon.sprite = m_passData.icon;
            levelText.text = "��" + level + "��";
            active.SetActive(!m_passData.unlock);
        }
    }
    public void ShowPassEvent()
    {
        if (m_passData.unlock)
        {
            taskDescriptionPanel.gameObject.SetActive(true);
            taskDescriptionPanel.Init(m_passData);
        }
    }
}
