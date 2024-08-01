using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainTowerManager : MonoBehaviour
{
    [SerializeField] private Image fillArea;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] float maxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        SetSliderValue();
        SetHpTextUpdate();
    }

    public void TakeDamage(float amount)
    {
        if(!GameManager.Instance.IsGameStart)return;
        currentHealth -= amount;
        SetSliderValue();
        SetHpTextUpdate();
        DamageScaleUp();
        if (currentHealth<=0)
        {
            GameManager.Instance.TriggeredDieEvent();
        }
    }

    void DamageScaleUp()
    {
        transform.DOScale(1.1f,.1f).OnComplete(()=>
        {
            transform.DOScale(1, .1f);
        });
    }
    void SetHpTextUpdate() => healthText.text= "HP:" + currentHealth.ToString();
     void SetSliderValue() => fillArea.DOFillAmount(currentHealth / maxHealth,.2f);
}
