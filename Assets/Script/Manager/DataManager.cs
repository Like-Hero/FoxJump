using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("Datas")]
    public AchievementData_SO AchievementData;
    public InventoryData_SO InventoryData;
    public PassDatas_SO PassDatas;
    //public PlayerData_SO PlayerData;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        /*
         * ��ȡ�洢��Ҫ��Ϣ
         */
        SaveManager.Ins.Load();
    }
}
