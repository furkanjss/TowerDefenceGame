
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Mortar : TowerBase
{
   
    public GameObject projectilePrefab;
 

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
            if (collider.CompareTag("Path"))
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
            
        Vector3 targetRotation = new Vector3(-90, 0, angle - 90); // -90 if the z-axis should be aligned with the direction

        Quaternion targetQuaternion = Quaternion.Euler(targetRotation);
        transform.DORotateQuaternion(targetQuaternion, .1f);
    }


    private void ShootAtTarget(Transform target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = ObjectPoolManager.Instance.GetFromPool("Mortar");
            projectile.transform.position = transform.position;
            projectile.transform.rotation=Quaternion.identity;;        
            MortarProjectile projectileComponent = projectile.GetComponent<MortarProjectile>();
            if (projectileComponent != null)
            {
                projectileComponent.SetTarget(target);
                projectileComponent.SetDamage(damage);
                projectileComponent.SetProjectileSpeed(.4f);
                projectileComponent.SetExplosionRadius(range/3);
              
            }
        }
    }
}