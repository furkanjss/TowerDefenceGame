using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // public TowerManager towerManager;
    // public EnemyManager enemyManager;
    // public BoosterManager boosterManager;
    // public SaveManager saveManager;

    public int playerGold;
    public int playerScore;
    public static event Action OnCollectableChanged;   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AskToLoadGame();
    }

    private void AskToLoadGame()
    {
       
    }

 
   public void CollectGold(int amount)
   {
       playerGold += amount;
       OnCollectableChanged?.Invoke();
   } 
    private void OnApplicationQuit()
    {
       // saveManager.SaveGame();
    }

    // public void CollectBooster(GameObject coin,int rewardGold)
    // {
    //     StartCoroutine(GoldMovement(.3f,coin,rewardGold));
    // }
    // private IEnumerator GoldMovement(float time, GameObject coin,int rewardGold)
    // {
    //  
    //
    //     Vector3 startingPos = coin.transform.position;
    //     Vector3 finalPos = Camera.main.ScreenToWorldPoint(new Vector3(topPanel.transform.position.x, topPanel.transform.position.y, Camera.main.nearClipPlane * 35));
    //     float elapsedTime = 0;
    //     while (elapsedTime < time)
    //     {
    //         finalPos = Camera.main.ScreenToWorldPoint(new Vector3(topPanel.transform.position.x, topPanel.transform.position.y, Camera.main.nearClipPlane * 35));
    //         coin.transform.position = Vector3.Lerp(startingPos, finalPos, elapsedTime / time);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //
    //     float totalSliderFillAmount = 0.175f;
    //    
    //     GameManager.Instance.playerGold += rewardGold;
    //    
    //     Destroy(coin);
    // }
}
