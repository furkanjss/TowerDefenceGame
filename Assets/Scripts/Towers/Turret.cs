
using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : TowerBase
{

    [SerializeField] private ParticleSystem shotEffect;
    public GameObject projectilePrefab; // Mermi prefab'ı

    private Transform targetEnemy;

  
   protected override void Start()
    {
       base.Start();
        StartCoroutine(DetonateAfterDelay(cooldown));
    }
    private IEnumerator DetonateAfterDelay(float delay)
    {
        yield return new WaitUntil(GetIsSettled);
        yield return new WaitForSeconds(delay);
        while (canShot)
        {
            ScaleUpEffect();
            Attack(); 
            yield return new WaitForSeconds(delay);
        }
       
    }
    protected override void Attack()
    {
        FindTarget();

        if (targetEnemy != null)
        {
            RotateTowardsTarget();
            shotEffect.Play();
            ShootAtTarget(targetEnemy);
            targetEnemy = null;
        }
    }

    private void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null&&damageable.IsEnemyAlive())
            {
                Transform enemyTransform = collider.transform;
                float distance = Vector3.Distance(transform.position, enemyTransform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemyTransform;
                    
                }
            }
        }

        targetEnemy = closestEnemy;
    }


    private void RotateTowardsTarget()
    {
      
        Vector3 direction = (targetEnemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
        Vector3 targetRotation = new Vector3(-90, 0, angle - 90);

        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
        transform.DORotateQuaternion(targetQuaternion, .1f);
    }

  
    private void ShootAtTarget(Transform target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = ObjectPoolManager.Instance.GetFromPool("Turret");
            projectile.transform.position = transform.position;
            projectile.transform.rotation=quaternion.identity;
            
            TurretProjectile projectileComponent = projectile.GetComponent<TurretProjectile>();
            if (projectileComponent != null)
            {
                projectileComponent.SetTarget(target);
                projectileComponent.SetDamage(damage);
                projectileComponent.SetProjectileSpeed(3);
              
            }
        }
    }

    
}