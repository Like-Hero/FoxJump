using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    private PlayerController player;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public bool Save()
    {
        UpdateAchievement();
        UpdateInventory();
        //保存玩家成就
        SaveAchievement(DataManager.Ins.AchievementData);
        //保存玩家背包
        SaveInventoryData(DataManager.Ins.InventoryData);
        //保存玩家关卡数据
        SavePassData(DataManager.Ins.PassDatas);
        return true;
    }
    public void Load()
    {
        LoadInventoryData(DataManager.Ins.InventoryData);
        LoadAchievement(DataManager.Ins.AchievementData);
        LoadPassData(DataManager.Ins.PassDatas);
    }
    /// <summary>
    /// 每次过关时调用，用来把该关卡捡拾到的道具加入到玩家背包
    /// </summary>
    private void UpdateInventory()
    {
        //把该关卡获得的道具加入到背包中
        DataManager.Ins.InventoryData.CalculateItemCount("Cherry", GameManager.Ins.currentPassData.cherry);
        DataManager.Ins.InventoryData.CalculateItemCount("Gem", GameManager.Ins.currentPassData.gem);
        DataManager.Ins.InventoryData.CalculateItemCount("Money", GameManager.Ins.currentPassData.money);
    }
    /// <summary>
    /// 更新当前成就
    /// </summary>
    private void UpdateAchievement()
    {
        //更新当前成就 
        ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("樱桃小丸子", GameManager.Ins.currentPassData.cherry);
        ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("钻石王老五", GameManager.Ins.currentPassData.gem);
        ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("杀手", GameManager.Ins.currentPassData.killedEnemy);
        ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("青蛙杀手", GameManager.Ins.currentPassData.killedFrog);
        ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("鼠妇杀手", GameManager.Ins.currentPassData.killedOpossum);
        ShowAchievementPopPanelManager.Ins.UpdateAchievementProgressWithAddUp("老鹰杀手", GameManager.Ins.currentPassData.killedEagle);
    }
    public void SaveAchievement(AchievementData_SO data)
    {
        SaveData(data, "PlayerAchievementData");
    }
    public bool LoadAchievement(AchievementData_SO data)
    {
        return LoadData(data, "PlayerAchievementData");
    }
    public void SaveInventoryData(InventoryData_SO data)
    {
        SaveData(data, "InventoryData");
    }
    public bool LoadInventoryData(InventoryData_SO data)
    {
        return LoadData(data, "InventoryData");
    }
    public void SavePassData(PassDatas_SO data)
    {
        SaveData(data, "PassData");
    }
    public bool LoadPassData(PassDatas_SO data)
    {
        return LoadData(data, "PassData");
    }
    public void SavePlayerData(PlayerData_SO data)
    {
        SaveData(data, "PlayerData");
    }
    public bool LoadPlayerData(PlayerData_SO data)
    {
        return LoadData(data, "PlayerData");
    }
    private void SaveData(Object data, string key)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }
    private bool LoadData(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
            return true;
        }
        return false;
    }
}
