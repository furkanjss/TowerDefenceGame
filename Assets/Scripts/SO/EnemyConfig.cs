using UnityEngine;


[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig", order = 3)]
public class EnemyConfig : ScriptableObject
{
    public string enemyName;
    public float health;
    public float speed;
    public float power;
    public int rewardGold;
    public int rewardScore;
    public Color color;
}
