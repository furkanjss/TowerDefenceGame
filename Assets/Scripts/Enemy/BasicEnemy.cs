using System;
using DG.Tweening;
using UnityEngine;
public class BasicEnemy : EnemyBase
{
    private Tween movementTween;
    [SerializeField]
    private Renderer _renderer;
     void Start()
     {
      
        MoveToTower(playerTower);
        
    }

    protected override void MoveToTower(Transform playerTower)
    {
        animator.SetBool("IsMoving", true); 
       movementTween= transform.DOMove(playerTower.position, speed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() =>
        {
          //  throwablePoolManager.ReturnObjectToPool(this);
        });
    }

    protected override void Die()
    {
        base.Die();
        movementTween.Kill();
        _renderer.material.DOColor(Color.black, .2f);
    }
}