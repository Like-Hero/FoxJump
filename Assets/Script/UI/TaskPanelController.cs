using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskPanelController : MonoBehaviour
{
    public List<Transform> Stars;
    public List<Transform> Success;
    public List<Text> DescritionsText;
    private PassData m_currentPassData;
    /// <summary>
    /// ���±��ι������ݻ������й�������
    /// </summary>
    /// <param name="passData"></param>
    public void Init(PassData passData)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = passData.icon;
        //����UI
        for (int i = 0; i < passData.tasks.Count; i++)
        {
            TaskData taskData = passData.tasks[i];
            //��ǰ�������ݺ����������������Ƚϣ�֮ǰ��������������������񶼸�������Ϊ���
            if (m_currentPassData != null && passData.level == m_currentPassData.level)
            {
                m_currentPassData.tasks[i].unlock = m_currentPassData.tasks[i].unlock || taskData.unlock;
            }
            Stars[i].Find("Active").gameObject.SetActive(!taskData.unlock);
            Success[i].Find("Active").gameObject.SetActive(!taskData.unlock);
            string descrition = "";
            switch (taskData.taskType)
            {
                case TaskType.AccessPass:
                    descrition = "ͨ��";
                    break;
                case TaskType.TimeLimit:
                    descrition = string.Format($"��{taskData.value}����ͨ��");
                    break;
                case TaskType.KilledEnemy:
                    descrition = string.Format($"����{taskData.value}������");
                    break;
                case TaskType.GetCherry:
                    descrition = string.Format($"�ռ�{taskData.value}��ӣ��");
                    break;
                case TaskType.GetGem:
                    descrition = string.Format($"�ռ�{taskData.value}����ʯ");
                    break;
            }
            DescritionsText[i].text = descrition;
        }
        //��ֹ���ݸ���
        if(m_currentPassData == null)
        {
            m_currentPassData = passData;
        }
    }
    /// <summary>
    /// ����ʱ����
    /// </summary>
    public void AccessPass()
    {
        m_currentPassData = DataManager.Ins.PassDatas.passDatas[SceneManager.GetActiveScene().buildIndex - 1];
        m_currentPassData.unlock = true;
        PassData tmp_passData = new PassData(m_currentPassData);
        //���¹ؿ�������Ϣ
        for (int i = 0; i < m_currentPassData.tasks.Count; i++)
        {
            TaskData taskData = tmp_passData.tasks[i];
            switch (taskData.taskType)
            {
                case TaskType.AccessPass:
                    taskData.unlock = true;
                    break;
                case TaskType.TimeLimit:
                    taskData.unlock = GameManager.Ins.currentPassData.accessTime <= taskData.value;
                    break;
                case TaskType.KilledEnemy:
                    taskData.unlock = GameManager.Ins.currentPassData.killedEnemy >= taskData.value;
                    break;
                case TaskType.GetCherry:
                    taskData.unlock = GameManager.Ins.currentPassData.cherry >= taskData.value;
                    break;
                case TaskType.GetGem:
                    taskData.unlock = GameManager.Ins.currentPassData.gem >= taskData.value;
                    break;
            }
        }
        int nextLevel;
        //������һ��
        if ((nextLevel = m_currentPassData.level + 1) <= SceneManager.sceneCountInBuildSettings)
        {
            DataManager.Ins.PassDatas.passDatas[nextLevel - 1].unlock = true;
        }
        //��ʼ����ǰ�ؿ���Ϣ
        Init(tmp_passData);
    }
    //�����ѡ��ؿ�����ѡ��
    public void LoadAppointPassEvent()
    {
        if (SceneController.IsInitialized)
        {
            if (ShowAchievementPopPanelManager.IsInitialized)
            {
                ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("ʱ�������", 1);
            }
            SceneController.Ins.TransitionLevel(m_currentPassData.level);
        }
    }
    //���ͨ��ѡ��
    public void LoadNextPassEvent()
    {
        if (SceneController.IsInitialized && (m_currentPassData.level + 1) <= SceneManager.sceneCountInBuildSettings)
        {
            if (ShowAchievementPopPanelManager.IsInitialized)
            {
                ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("ʱ�������", 1);
            }
            SceneController.Ins.TransitionNextScene();
        }
    }
    public void ToMainEvent()
    {
        if (SceneController.IsInitialized)
        {
            SceneController.Ins.LoadMain();
        }
    }
}
