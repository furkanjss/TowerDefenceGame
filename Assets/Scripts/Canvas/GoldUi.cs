using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUi : MonoBehaviour
{
   private TextMeshProUGUI coinText;

   private void Start()
   {
      coinText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
      UpdateText();
   }

   void UpdateText() => coinText.text = GameManager.Instance.playerGold.ToString();

   private void OnEnable()
   {
      GameManager.OnCollectableChanged += UpdateText;
   }  private void OnDisable()
   {
      GameManager.OnCollectableChanged -= UpdateText;
   }
}
