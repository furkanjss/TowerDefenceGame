using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    public List<TowerData> towers = new List<TowerData>();
    public List<UpgradeData> upgrades = new List<UpgradeData>();
    public int gold;
    public int currentScore;
    public float currentSpawnInterval;
    public float boosterValue;
}

[System.Serializable]
public class TowerData
{
    public string towerName;
    public int slotId;
}
[System.Serializable]
public class UpgradeData
{
    public string towerName;
    public int price;
}