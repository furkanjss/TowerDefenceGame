using System;
using DG.Tweening;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour,IDamageable
{
    protected EnemyConfig config;
    protected float currentHealth;
    protected float speed;
    protected float power;
    protected Animator animator;
    protected Transform playerTower;
    private bool isAlive = true;
    public void Initialize(EnemyConfig config,Transform target)
    {
        this.config = config;
        this.currentHealth = config.health;
        this.speed = config.speed;
        this.playerTower = target;
        this.power = config.power;
        GetAnimator();
    }

    public bool IsEnemyAlive() => isAlive;

    protected abstract void MoveToTower(Transform playerTower,MainTowerManager mainTowerManager);


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
            isAlive = false;
            Die();
        }
        else
        {
            ScaleUpEffect();
        }
    }

    void ScaleUpEffect()
    {
        transform.DOScale(1.5f,.1f).OnComplete(() =>
        {
            transform.DOScale(1, .1f);
        });
    }
    protected virtual void Die()
    {
        
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        GameManager.Instance.CollectGoldAndScore(config.rewardGold,config.rewardScore);

        transform.DOScale(Vector3.zero, 1f)
            .SetDelay(2)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}