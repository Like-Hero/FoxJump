using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 同时达成多个成就时，让成就动画一个一个播放
/// </summary>
public class ShowAchievementPanelUtility
{
    private Queue<AchievementData> _achievementItemsQueue;//存的是具体的成就信息
    public ShowAchievementPanelUtility()
    {
        _achievementItemsQueue = new Queue<AchievementData>();
    }
    /// <summary>
    /// 成就达成时则调用，会自动按照顺序一个一个播放成就动画
    /// </summary>
    /// <param name="achievement"></param>
    public void Execute(AchievementData achievement)
    {
        _achievementItemsQueue.Enqueue(achievement);
    }
    /// <summary>
    /// 在Update里面调用，以保证正确播放成就动画
    /// </summary>
    public void Update(AchievementPopPanel achievementPanel)
    {
        //如果还有成就在队列，则运行
        if (_achievementItemsQueue.Count >= 1)
        {
            //如果执行完成就达成的动画，也就说明Panel为空，就出队列，让下一个成就动画出来
            //如果动画没有播放完，则跳过
            if (achievementPanel == null)
            {
                achievementPanel = ShowAchievementPopPanelManager.Ins.InstantiateNewAchievementPanel();
                achievementPanel.SetNewAchievementPanel(_achievementItemsQueue.Dequeue());
            }
        }
    }
}
