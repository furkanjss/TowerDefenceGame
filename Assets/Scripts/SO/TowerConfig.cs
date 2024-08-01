using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "ScriptableObjects/TowerConfig", order = 1)]
public class TowerConfig : ScriptableObject
{
    public string towerName;
    public float range;
    public float damage;
    public float fireRate;
    public GameObject towerPrefab;
    public int priceIncrease = 5;
}
