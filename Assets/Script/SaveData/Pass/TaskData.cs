using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType
{
    AccessPass,//ͨ��
    TimeLimit,//�涨ʱ��ͨ�أ���λ����
    KilledEnemy,//ɱ�����˸���
    GetCherry,//��ʰӣ�Ҹ���
    GetGem,//��ʰ��ʯ����
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
