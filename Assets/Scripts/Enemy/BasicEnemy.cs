using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicEnemy : EnemyBase
{
    private Tween movementTween;
    [SerializeField]
    private Renderer _renderer;
     void Start()
     {
      
        MoveToTower(playerTower,playerTower.GetComponent<MainTowerManager>());
        _renderer.material.color= config.color;
     }

    protected override void MoveToTower(Transform playerTower,MainTowerManager mainTowerManager)
    {
        animator.SetBool("IsMoving", true); 
       movementTween= transform.DOMove(playerTower.position, speed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() =>
        {
          mainTowerManager.TakeDamage(power);
        });
    }


    protected override void Die()
    {
        base.Die();
        movementTween.Kill();
            DieColorEffect();
            GameManager.Instance.CollectBooster();

    }
    void DieColorEffect()=>        _renderer.material.DOColor(Color.black, .2f);

}