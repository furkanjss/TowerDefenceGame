using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    protected TowerConfig config;
    protected float cooldown;

    public void Initialize(TowerConfig config)
    {
        this.config = config;
        cooldown = 1 / config.fireRate;
    }

    protected abstract void Attack();
}