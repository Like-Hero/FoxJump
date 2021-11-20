using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AchievementData
{
    public int achievementId;
    public AchievementType achievementType;
    public string achievementName;
    public string achievementDescription;
    public Sprite achievementIcon;
    public Sprite awardIcon;
    public int currentProgress;//玩家成就进度
    public int maxProgress;//玩家达到成就的阈值
    public bool unlocked;//成就是否达成
    public bool isHide;//是否时隐藏成就
    [Header("奖励")]
    public ItemType awardType;
    public int awardAmount;//奖励数
    public bool isReceiveAward;//是否领取过奖励
    /*
     * 下方参数对应不同的类型
     */
    [Header("根据不同的成就类型选择")]
    public EnemyType enemyType;
    public ItemType itemType;
}

/// <summary>
/// 因为最大数类别的数据需要根据具体数据判断，比如最大血量就需要当前玩家的最大血量，所以每一种类别都需要一个变量来存储
/// 但是累计这一类数据只需要一个类别：AddUp就可以了，只是需要每一个地方自己做判断而不是在AchievementManager里面一直监听
/// </summary>
public enum AchievementType
{
    CherryAmountMax,//玩家单局樱桃的最大数量
    GemAmountMax,//玩家单局钻石的最大数量
    MoneyAmountMax,//玩家单局钱币的最大数量
    CherryAmountAddUp,//玩家累计樱桃的数量
    GemAmountAddUp,//玩家累计钻石的数量
    MoneyAmountAddUp,//玩家累计钱币的数量
    KilledEmemyAddUp,//累计击杀敌人
    ReStartGameAddUp,//玩家累计重新游玩次数
    GetItem,
    //AddUp,//玩家累计（钱、击杀敌人数目、物品数量等等）类别
}