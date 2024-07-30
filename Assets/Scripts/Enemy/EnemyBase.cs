using System;
using DG.Tweening;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected EnemyConfig config;
    protected float currentHealth;
    protected float speed;
    protected Animator animator;
    protected Transform playerTower;
    public bool die;
    public void Initialize(EnemyConfig config,Transform target)
    {
        this.config = config;
        this.currentHealth = config.health;
        this.speed = config.speed;
        this.playerTower = target;
        GetAnimator();
    }


    private void Update()
    {
        if (die)
        {
            die = false;
            Die();
        }
    }

    protected abstract void MoveToTower(Transform playerTower);


    void GetAnimator()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found ");
        }
    }  
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        GameManager.Instance.CollectGold(config.rewardGold);

        transform.DOScale(Vector3.zero, 1f)
            .SetDelay(2)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}