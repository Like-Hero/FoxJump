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
    /// 更新本次过关数据或者已有过关数据
    /// </summary>
    /// <param name="passData"></param>
    public void Init(PassData passData)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = passData.icon;
        //更新UI
        for (int i = 0; i < passData.tasks.Count; i++)
        {
            TaskData taskData = passData.tasks[i];
            //当前任务数据和以往任务数据作比较，之前完成任务或者现在完成任务都更新数据为完成
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
                    descrition = "通关";
                    break;
                case TaskType.TimeLimit:
                    descrition = string.Format($"在{taskData.value}秒内通关");
                    break;
                case TaskType.KilledEnemy:
                    descrition = string.Format($"击败{taskData.value}个敌人");
                    break;
                case TaskType.GetCherry:
                    descrition = string.Format($"收集{taskData.value}个樱桃");
                    break;
                case TaskType.GetGem:
                    descrition = string.Format($"收集{taskData.value}个钻石");
                    break;
            }
            DescritionsText[i].text = descrition;
        }
        //防止数据覆盖
        if(m_currentPassData == null)
        {
            m_currentPassData = passData;
        }
    }
    /// <summary>
    /// 过关时调用
    /// </summary>
    public void AccessPass()
    {
        m_currentPassData = DataManager.Ins.PassDatas.passDatas[SceneManager.GetActiveScene().buildIndex - 1];
        m_currentPassData.unlock = true;
        PassData tmp_passData = new PassData(m_currentPassData);
        //更新关卡任务信息
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
        //解锁下一关
        if ((nextLevel = m_currentPassData.level + 1) <= SceneManager.sceneCountInBuildSettings)
        {
            DataManager.Ins.PassDatas.passDatas[nextLevel - 1].unlock = true;
        }
        //初始化当前关卡信息
        Init(tmp_passData);
    }
    //玩家在选择关卡界面选择
    public void LoadAppointPassEvent()
    {
        if (SceneController.IsInitialized)
        {
            if (ShowAchievementPopPanelManager.IsInitialized)
            {
                ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("时间回溯者", 1);
            }
            SceneController.Ins.TransitionLevel(m_currentPassData.level);
        }
    }
    //玩家通后选择
    public void LoadNextPassEvent()
    {
        if (SceneController.IsInitialized && (m_currentPassData.level + 1) <= SceneManager.sceneCountInBuildSettings)
        {
            if (ShowAchievementPopPanelManager.IsInitialized)
            {
                ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("时间回溯者", 1);
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
