using System.Collections;
using UnityEngine;
public class Mine : TowerBase
{
  
    public GameObject explosionEffect;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DetonateAfterDelay(cooldown));
    }

    private IEnumerator DetonateAfterDelay(float delay)
    {
        yield return new WaitUntil(GetIsSettled);
        yield return new WaitForSeconds(delay);
        Attack(); 
    }

    protected override void Attack()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        
        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}