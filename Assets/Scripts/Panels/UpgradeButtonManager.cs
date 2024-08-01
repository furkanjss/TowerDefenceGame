using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonManager : MonoBehaviour
{
   [SerializeField] private TowerConfig towerConfig;
   [SerializeField] private TextMeshProUGUI priceText;
  public int price;
   [SerializeField]  private Button _button;

   private void Start()
   {
      _button.onClick.AddListener(CreateTower);
      SetInitializePrice();
   }
   private void CreateTower()
   {
      if (  towerConfig != null)
      {
         GameObject tower = Instantiate(towerConfig.towerPrefab);
   
         int oldPrice = price;
         price += towerConfig.priceIncrease;
         PriceTextUpdate();
  
         GameManager.Instance.SpendMoney(oldPrice);
 
       
      }
   }


   public void SetInitializePrice()
   {
      price = 0;
      PriceTextUpdate();
   }
   public void GetLoadingPrice()
   {
      price = GameManager.Instance.GetCurrentPrice(towerConfig);
      if (priceText==null)
      {
         priceText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
      }
      PriceTextUpdate();
      priceText.text = price.ToString();

   }
   public void PriceTextUpdate() => priceText.text = price.ToString();
   void UpgradeIsActive()
   {
    
      if (price>GameManager.Instance.playerGold)
      {
         _button.interactable = false;
      }
      else
      {
         _button.interactable = true;

      }
   }

   private void OnEnable()
   {
      GameManager.OnGameDataLoad += GetLoadingPrice;
      GameManager.OnCollectableChanged += UpgradeIsActive;
   } private void OnDisable()
   {
      GameManager.OnGameDataLoad -= GetLoadingPrice;
   
      GameManager.OnCollectableChanged -= UpgradeIsActive;
   }

   public TowerConfig GetConfig() => towerConfig;

  
}
