using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    protected Transform target;
    protected float damage;
    protected float projectileSpeed;
    public string poolTag;
 

    protected abstract void MoveTowardsTarget();

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetProjectileSpeed(float speed)
    {
        this.projectileSpeed = speed;
    }
public void ReturnToPool()=>            ObjectPoolManager.Instance.ReturnToPool(poolTag, gameObject);

    protected abstract void Explode();
    protected abstract void SetDamage();
}