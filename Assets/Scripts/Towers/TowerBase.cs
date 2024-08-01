using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TowerBase : MonoBehaviour
{
    public TowerConfig config;
    protected float cooldown;
    protected bool isSettled = false;
    protected TowerSlotHolder settleSlot;
    protected float damage = 50f;
    protected float range = 50f;
    protected bool canShot = true;
    protected float initialScale;
    public GameObject boosterEffect;

    protected virtual void Start()
    {
        Initialize(config);
        initialScale = transform.localScale.x;
    }

    public void Initialize(TowerConfig config)
    {
        cooldown =  config.fireRate;
        range = this.config.range;
        this.damage = this.config.damage;
    }
    
    protected abstract void Attack();
    public bool GetIsSettled()
    {
        return isSettled;
    }

    public void SetIsSettled(bool value,TowerSlotHolder slotHolder)
    {
        isSettled = value;
        settleSlot = slotHolder;
        transform.SetParent(settleSlot.transform);
        transform.localPosition=new Vector3(0,-.25f,.2f);
        
    }

    public int GetParentId()
    {
        
            return settleSlot.slotId;
        
        
    }

    protected virtual void OnEnable()
    {
        BoosterPanel.OnBoosterActivated += BoostTimeActive;
        GameManager.OnPlayerLoseWar += DisableTurret;

    } protected virtual void OnDisable()
    {
        BoosterPanel.OnBoosterActivated -= BoostTimeActive;
        GameManager.OnPlayerLoseWar -= DisableTurret;

    }

    public void BoostTimeActive()
    {
        boosterEffect.gameObject.SetActive(true);
        cooldown = cooldown / 5;
        DOVirtual.DelayedCall(5, () =>
        {
            cooldown = config.fireRate;
            boosterEffect.gameObject.SetActive(false);

        });
    }

    public void ScaleUpEffect()
    {
       
            transform.DOScale(initialScale + 4f, .1f).OnComplete(() =>
            {
                transform.DOScale(initialScale, .1f);
            });
        
    }
    public void DisableTurret() =>canShot=false;
}