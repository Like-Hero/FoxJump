using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassData
{
    public int level;
    public Sprite icon;
    public List<TaskData> tasks;
    public bool unlock;

    public PassData(PassData passData)
    {
        level = passData.level;
        icon = passData.icon;
        tasks = new List<TaskData>();
        foreach (TaskData item in passData.tasks)
        {
            tasks.Add(new TaskData(item));
        }
        unlock = passData.unlock;
    }
}
