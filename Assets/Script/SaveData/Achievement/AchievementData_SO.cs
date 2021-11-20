using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievements Data", menuName = "Achievements")]
[Serializable]
public class AchievementData_SO : ScriptableObject
{
    public List<AchievementData> achievements;
    public AchievementData GetAchievement(int achievementId)
    {
        return FindAchievement(achievementId);
    }
    public AchievementData GetAchievement(String achievementName)
    {
        return FindAchievement(achievementName);
    }
    private AchievementData FindAchievement(int achievementId)
    {
        if (achievementId < 0)
        {
            return null;
        }
        foreach (AchievementData achievementData in achievements)
        {
            if (achievementData != null && achievementData.achievementId == achievementId)
            {
                return achievementData;
            }
        }
        return null;
    }
    private AchievementData FindAchievement(string achievementName)
    {
        if (achievementName.Equals(""))
        {
            return null;
        }
        foreach (AchievementData achievementData in achievements)
        {
            if (achievementData != null && achievementData.achievementName == achievementName)
            {
                return achievementData;
            }
        }
        return null;
    }
}
