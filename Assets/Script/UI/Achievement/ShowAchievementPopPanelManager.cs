using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAchievementPopPanelManager : Singleton<ShowAchievementPopPanelManager>
{
    [Header("UI Setting")]
    public AchievementPopPanel achievementPopPanel;
    private AchievementPopPanel _achievementPanel;
    private ShowAchievementPanelUtility panelUtility;
    private AchievementData_SO _achievement_SO;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        panelUtility = new ShowAchievementPanelUtility();
    }
    private void Start()
    {
        _achievement_SO = DataManager.Ins.AchievementData;
        //说明玩家重开了一次
        if(PlayerPrefs.HasKey("GameIsContinue") && PlayerPrefs.GetInt("GameIsContinue") == 0)
        {
            UpdateAchievementProgressWithAddUp("时间回溯者", 1);
        }
    }
    private void Update()
    {
        //如果是教学关卡则不管
        if (PlayerPrefs.GetInt("Teaching") == 1)
            return;
        if (GameManager.IsInitialized && GameManager.Ins.Player != null && _achievement_SO != null)
        {
            foreach (AchievementData achievement in _achievement_SO.achievements)
            {
                if (achievement.unlocked)
                {
                    continue;
                }
                switch (achievement.achievementType)
                {
                    case AchievementType.CherryAmountMax:
                        UpdateAchievementProgressWithMax(achievement, GameManager.Ins.currentPassData.cherry);
                        break;
                    case AchievementType.GemAmountMax:
                        UpdateAchievementProgressWithMax(achievement, GameManager.Ins.currentPassData.cherry);
                        break;
                }
            }
            panelUtility.Update(_achievementPanel);
        }
    }
    private void ObtainAchievement(AchievementData achievement)
    {
        if (achievement != null && !achievement.unlocked)
        {
            achievement.unlocked = true;
            panelUtility.Execute(achievement);
        }
    }
    private void ObtainAchievement(int achievementId)
    {
        if (achievementId < 0 || achievementId >= _achievement_SO.achievements.Count || _achievement_SO.achievements[achievementId].unlocked)
            return;
        AchievementData achievement = _achievement_SO.achievements[achievementId];
        achievement.unlocked = true;
        panelUtility.Execute(achievement);
    }
    private void UpdateAchievementProgressWithMax(AchievementData achievement, int value)
    {
        achievement.currentProgress = Mathf.Clamp(value, 0, achievement.maxProgress);
        if (value >= achievement.maxProgress)
        {
            ObtainAchievement(achievement);
        }
    }
    private void UpdateAchievementProgressWithMax(int achievementId, int value)
    {
        if (achievementId < 0 || achievementId >= _achievement_SO.achievements.Count)
            return;
        _achievement_SO.achievements[achievementId].currentProgress =
            Mathf.Clamp(value, 0, _achievement_SO.achievements[achievementId].maxProgress);
    }
    private void UpdateAchievementProgressWithMax(string achievementName, int value)
    {
        AchievementData achievement = FindAchievementWithName(achievementName);
        if (achievement != null && !achievement.unlocked)
        {
            achievement.currentProgress = Mathf.Clamp(value, 0, achievement.maxProgress);
        }
    }
    public void UpdateAchievementProgressWithAddUp(AchievementData achievement, int value)
    {
        if (achievement != null && !achievement.unlocked)
        {
            achievement.currentProgress = Mathf.Clamp(achievement.currentProgress + value, 0, achievement.maxProgress);
            if (achievement.currentProgress >= achievement.maxProgress)
            {
                ObtainAchievement(achievement);
            }
        }
    }
    public void UpdateAchievementProgressWithAddUp(int achievementId, int value)
    {
        if (achievementId >= 0 && achievementId < _achievement_SO.achievements.Count || !_achievement_SO.achievements[achievementId].unlocked)
        {
            AchievementData achievement = _achievement_SO.achievements[achievementId];
            achievement.currentProgress = Mathf.Clamp(achievement.currentProgress + value, 0, achievement.maxProgress);
            if (achievement.currentProgress >= achievement.maxProgress)
            {
                ObtainAchievement(achievement);
            }
        }
    }
    public void UpdateAchievementProgressWithAddUp(string achievementName, int value)
    {
        AchievementData achievement = FindAchievementWithName(achievementName);
        if (achievement != null && !achievement.unlocked)
        {
            achievement.currentProgress = Mathf.Clamp(achievement.currentProgress + value, 0, achievement.maxProgress);
            if (achievement.currentProgress >= achievement.maxProgress)
            {
                ObtainAchievement(achievement);
            }
        }
    }
    private AchievementData FindAchievementWithName(string acievementName)
    {
        foreach (AchievementData item in _achievement_SO.achievements)
        {
            if (item.achievementName == acievementName)
            {
                return item;
            }
        }
        return null;
    }
    public AchievementPopPanel InstantiateNewAchievementPanel()
    {
        return _achievementPanel = Instantiate(achievementPopPanel, GameObject.Find("Canvas").transform);
    }
}
