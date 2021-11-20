using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Player Data", menuName = "PlayerData")]
[Serializable]
public class PlayerData_SO : ScriptableObject
{
    public int cheeryCount;
    public int gemCount;
    public int money;
}
