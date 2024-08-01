using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoosterPanel : MonoBehaviour
{
    [SerializeField] private Image fillArea;
   private Button boosterButton;
   [SerializeField]
   private GameObject boosterActiveText; 
   [SerializeField]
   private TextMeshProUGUI percentageText;
    [SerializeField] private float boosterNeedAmount;
    private float boosterCurrentAmount;
    public static Action OnBoosterActivated;

    private void Start()
    {
        boosterButton = GetComponent<Button>();
        boosterButton.onClick.AddListener(BoosterEventTriggered);

    }

    void GetCollectedBoosterValue() => FillBooster(GameManager.Instance.loadedData.boosterValue);
    public void FillBooster(float value)
    {
        
        boosterCurrentAmount += value;
        if (boosterCurrentAmount>boosterNeedAmount)
        {
            boosterCurrentAmount = boosterNeedAmount;
        }
        percentageText.text = "BOOSTER: " + boosterCurrentAmount.ToString();

        fillArea.fillAmount = boosterCurrentAmount/boosterNeedAmount;
        if (boosterCurrentAmount>=boosterNeedAmount)
        {
            boosterButton.interactable = true;
            boosterActiveText.gameObject.SetActive(true);
        }
        
    }

    void BoosterEventTriggered() => OnBoosterActivated?.Invoke();

    void RefreshBooster()
    {
        fillArea.fillAmount = 0;
        boosterButton.interactable = false;
        boosterActiveText.SetActive(false);
        boosterCurrentAmount = 0;
        transform.DOScale(1.1f, .1f).OnComplete(() =>
        {
            transform.DOScale(1, .1f);
        });
    }
    private void OnEnable()
    {
        OnBoosterActivated += RefreshBooster;
        GameManager.OnGameDataLoad += GetCollectedBoosterValue;
    } private void OnDisable()
    {
        GameManager.OnGameDataLoad -= RefreshBooster;
    }

    public float GetCurrentBooster() => boosterCurrentAmount;
}
