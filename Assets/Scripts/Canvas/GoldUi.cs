using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldUi : MonoBehaviour
{
   [SerializeField]
   private TextMeshProUGUI coinText;

  

   void UpdateText()
   {
      coinText.text = GameManager.Instance.playerGold.ToString();
     coinText.ForceMeshUpdate();
    
   }

   private void Update()
   {
      coinText.text = GameManager.Instance.playerGold.ToString();
   }

   private void OnEnable()
   {
      GameManager.OnCollectableChanged += UpdateText;
   }

   private void OnDisable()
   {
      GameManager.OnCollectableChanged -= UpdateText;
   }
}
