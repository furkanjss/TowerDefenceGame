using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotHolder : MonoBehaviour
{
  public int slotId;
  public TowerBase holdTower;


  public void PlaceTower(TowerBase tower)
  {
    if (CanTowerPlace())
    {
      holdTower = tower;
      
      tower.transform.SetParent(transform);
      tower.transform.localPosition = Vector3.zero;
      GameManager.Instance.IsGameStart = true;
    }
    else
    {
      Debug.LogWarning("Slot already holds a tower.");
    }
  }
  
  public bool CanTowerPlace()
  {
    return holdTower == null;
  }
  
}
