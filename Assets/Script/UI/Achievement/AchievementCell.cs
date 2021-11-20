using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementCell : MonoBehaviour
{
    private AchievementData _achievement;
    public Image NamePanel;
    public Image awardImage;
    public Image icon;
    public Text nameText;
    public Text descriptionText;
    public Text currentProgressText;
    public Slider progress;
    public Text awardAmountText;
    public Button receiveButton;
    public GameObject active;
    [Header("Sprite")]
    public Sprite UnlockNamePanelSprite;
    public Sprite LockNamePanelSprite;
    public Sprite HideNamePanelSprite;
    public Sprite HideIcon;
    public void Init(AchievementData achievement)
    {
        _achievement = achievement;
        //如果时隐藏成就并且没有被解锁
        if (_achievement.isHide && !_achievement.unlocked)
        {
            icon.sprite = HideIcon;
            awardImage.sprite = HideIcon;
            nameText.text = "未知";
            descriptionText.text = "隐藏成就，需要发现";
            currentProgressText.text = "0";
            progress.value = 0;
            awardAmountText.text = "???";
            active.SetActive(!_achievement.unlocked);
        }
        else
        {
            icon.sprite = _achievement.achievementIcon;
            nameText.text = _achievement.achievementName;
            awardImage.sprite = _achievement.awardIcon;
            descriptionText.text = _achievement.achievementDescription;
            currentProgressText.text = "(" + _achievement.currentProgress + " / " + _achievement.maxProgress + ")";
            progress.value = (float)_achievement.currentProgress / _achievement.maxProgress;
            awardAmountText.text = "X" + _achievement.awardAmount;
            active.SetActive(!_achievement.unlocked);
            if (_achievement.isHide)
            {
                NamePanel.sprite = HideNamePanelSprite;
            }
            else
            {
                NamePanel.sprite = _achievement.unlocked ? UnlockNamePanelSprite : LockNamePanelSprite;
            }
        }
        //如果该成就领取状态为true，设置Button不可以点击
        receiveButton.enabled = !_achievement.isReceiveAward;
        receiveButton.interactable = !_achievement.isReceiveAward;
        receiveButton.transform.GetChild(0).GetComponent<Text>().text = _achievement.isReceiveAward ? "已领取" : "领取奖励";
    }
    public void ReceiveAward()
    {
        receiveButton.transform.GetChild(0).GetComponent<Text>().text = "已领取";
        //设置该成就领取状态为true，设置Button不可以点击
        receiveButton.enabled = false;
        receiveButton.interactable = false;
        _achievement.isReceiveAward = true;
        //奖励数永久加入到玩家数据里
        DataManager.Ins.InventoryData.CalculateItemCount(_achievement.itemType, _achievement.awardAmount);
        //成就重新写入文件
        SaveManager.Ins.SaveAchievement(DataManager.Ins.AchievementData);
        //重新写入背包文件
        SaveManager.Ins.SaveInventoryData(DataManager.Ins.InventoryData);
    }
}
