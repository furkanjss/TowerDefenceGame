using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : BaseProjectile
{
    protected override void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * projectileSpeed * Time.deltaTime;
    }

    protected override void Explode()
    {
        
    }  
    protected override void SetDamage()
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        else
        {
            print("EnemyNotFound");
        }
    }
    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();

            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                
                SetDamage();
                Explode();
               ReturnToPool();
            }
        }
    }
}