using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAchievementSlotController : Singleton<ShowAchievementSlotController>
{
    public Transform ScrollContent;
    public AchievementCell AchievementCell;
    private void OnEnable()
    {
        RefreshAllAchievementSlot();
    }
    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }
    public void RefreshAllAchievementSlot()
    {
        //先删除所以的成就格子
        for (int i = 0; i < ScrollContent.childCount; i++)
        {
            Destroy(ScrollContent.GetChild(i).gameObject);
        }
        //遍历已有成就，然后加入到ScrollView
        foreach (AchievementData achievement in DataManager.Ins.AchievementData.achievements)
        {
            AchievementCell cell = Instantiate(AchievementCell);
            cell.Init(achievement);
            cell.transform.SetParent(ScrollContent);
        }
    }
}
