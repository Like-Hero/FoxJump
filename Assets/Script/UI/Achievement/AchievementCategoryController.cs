using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCategoryController : MonoBehaviour
{
    public AchievementCell achievementCell;
    public Transform ScrollContent;
    public AchievementType achievementType;
    public void RefreshAchievementPanelContentWithType()
    {
        //先删除所以的成就格子
        for (int i = 0; i < ScrollContent.childCount; i++)
        {
            Destroy(ScrollContent.GetChild(i).gameObject);
        }
        if (DataManager.IsInitialized)
        {
            foreach (AchievementData achievement in DataManager.Ins.AchievementData.achievements)
            {
                /*
                 * 1.如果该分类类型为GetItem，并且该成就是Item子类型（CherryAmountAddUp、GemAmountAddUp）等
                 * 2.如果分类类型和成就类型对应
                 * 3.并且类型对应并且情况1：如果是隐藏成就，那么必须要达成成就；或者类型对应并且情况2：如果不是隐藏成就，那么无需达成成就
                 */
                if ((achievement.achievementType == achievementType || 
                    achievementType == AchievementType.GetItem &&
                    (
                    achievement.achievementType == AchievementType.CherryAmountAddUp ||
                    achievement.achievementType == AchievementType.CherryAmountMax ||
                    achievement.achievementType == AchievementType.GemAmountAddUp ||
                    achievement.achievementType == AchievementType.GemAmountMax ||
                    achievement.achievementType == AchievementType.MoneyAmountAddUp ||
                    achievement.achievementType == AchievementType.MoneyAmountMax
                    )) && 
                    ((achievement.isHide && achievement.unlocked) || !achievement.isHide))
                {
                    AchievementCell cell = Instantiate(achievementCell);
                    cell.Init(achievement);
                    cell.transform.SetParent(ScrollContent);
                }
            }
        }
    }
    public void RefreshAchievementPanelContentWithUnlock(bool unlock)
    {
        //先删除所以的成就格子
        for (int i = 0; i < ScrollContent.childCount; i++)
        {
            Destroy(ScrollContent.GetChild(i).gameObject);
        }
        if (DataManager.IsInitialized)
        {
            foreach (AchievementData achievement in DataManager.Ins.AchievementData.achievements)
            {
                if (achievement.unlocked == unlock)
                {
                    AchievementCell cell = Instantiate(achievementCell);
                    cell.Init(achievement);
                    cell.transform.SetParent(ScrollContent);
                }
            }
        }
    }
    public void RefreshAchievementPanelContentWithHide()
    {
        //先删除所以的成就格子
        for (int i = 0; i < ScrollContent.childCount; i++)
        {
            Destroy(ScrollContent.GetChild(i).gameObject);
        }
        if (DataManager.IsInitialized)
        {
            foreach (AchievementData achievement in DataManager.Ins.AchievementData.achievements)
            {
                if (achievement.isHide)
                {
                    AchievementCell cell = Instantiate(achievementCell);
                    cell.Init(achievement);
                    cell.transform.SetParent(ScrollContent);
                }
            }
        }
    }

}
