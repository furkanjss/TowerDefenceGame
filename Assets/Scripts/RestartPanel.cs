using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartPanel : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    private void Start()
    {
        restartButton.onClick.AddListener(RestartScene);

    }
    void RestartScene()
    {
       
            GameManager.Instance.QuitGame();

        
            
    }
    void PanelActive() => transform.GetChild(0).gameObject.SetActive(true);

    private void OnEnable()
    {
        GameManager.OnPlayerLoseWar += PanelActive;
    } private void OnDisable()
    {
        GameManager.OnPlayerLoseWar -= PanelActive;
    }
}
