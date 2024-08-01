using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class MortarProjectile : BaseProjectile
{

    [SerializeField] private ParticleSystem explosioEffect;

    private float explosionRadius;

    public void SetExplosionRadius(float radius)
    {
        this.explosionRadius = radius;
    }
    private void Start()
    {  
     
        MoveTowardsTarget();
    }
    protected override void MoveTowardsTarget()
    {
        print(target.name);
        transform.DOJump(target.position, 1, 1, projectileSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            SetDamage();
            Explode();
            explosioEffect.Play();
            DOVirtual.DelayedCall(1, () =>
            {
                ReturnToPool();

            });
        });
    }

   
    protected override void Explode()
    {
        
    }  
    protected override void SetDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }

}
