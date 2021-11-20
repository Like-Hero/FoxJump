using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    AccessPass,//通关
    TimeLimit,//规定时间通关，单位：秒
    KilledEnemy,//杀死敌人个数
    GetCherry,//捡拾樱桃个数
    GetGem,//捡拾钻石个数
}

[System.Serializable]
public class TaskData
{
    public TaskType taskType;
    public int value;
    public bool unlock;
    public TaskData(TaskData taskData)
    {
        taskType = taskData.taskType;
        value = taskData.value;
        unlock = taskData.unlock;
    }
}
