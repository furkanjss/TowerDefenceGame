using UnityEngine;

[CreateAssetMenu(fileName = "ButtonConfig", menuName = "ScriptableObjects/ButtonConfig", order = 2)]
public class ButtonConfig : ScriptableObject
{
    public TowerConfig TowerConfig;
    public int basePrice;
    public int increasePrice;
}
