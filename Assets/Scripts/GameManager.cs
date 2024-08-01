using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SaveManager _saveManager;
    public static GameManager Instance { get; private set; }

    public SaveData loadedData;

    [SerializeField] private List<TowerSlotHolder> _towerSlotHolders = new List<TowerSlotHolder>();
    [SerializeField] private List<GameObject> towersPrefabs = new List<GameObject>();
    [SerializeField] private GameObject askLoadPanel;
    [SerializeField] private BoosterPanel boosterPanel;
   
    public int playerGold;
    public int playerScore;
    public bool IsGameStart;
    public static event  Action OnGameDataLoad;
    public static event  Action OnPlayerLoseWar;

    public static event Action OnCollectableChanged;   
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

    }


    private void OnEnable()
    {
        OnGameDataLoad += GetScoreAndCoinData;
        OnGameDataLoad += CreateLoadedTowers;
        OnPlayerLoseWar += GameStartToStop;
        OnPlayerLoseWar += _saveManager.SaveGame;
    }

    private void OnDisable()
    {
        OnGameDataLoad -= GetScoreAndCoinData;
        OnGameDataLoad -= CreateLoadedTowers;
        OnPlayerLoseWar -= GameStartToStop;
        OnPlayerLoseWar -= _saveManager.SaveGame;

    }

    public void AskToLoadGame()
    {
        askLoadPanel.SetActive(true);
    }

    public void LoadData()
    {
        _saveManager.LoadGame();
    }

    public void SpendMoney(int amount)
    {
        playerGold -= amount;
        OnCollectableChanged?.Invoke();
    }

    public void CollectGoldAndScore(int coinAmount, int scoreAmount)
    {
        playerGold += coinAmount;
        playerScore += scoreAmount;
        OnCollectableChanged?.Invoke();
    }

    private void OnApplicationQuit()
    {
        _saveManager.SaveGame();
    }

    public void GetLoadedData(SaveData data)
    {
        loadedData = data;
        DOVirtual.DelayedCall(1, () =>
        {
            OnGameDataLoad?.Invoke();
        });
    }

    public void GetScoreAndCoinData()
    {
        playerGold = loadedData.gold;
        playerScore = loadedData.currentScore;
        OnCollectableChanged?.Invoke();
    }

    public void CreateLoadedTowers()
    {
        StartCoroutine(CreateLoadedTowersCoroutine());
    }

    private IEnumerator CreateLoadedTowersCoroutine()
    {
        yield return new WaitUntil(() => loadedData != null);

        if (loadedData.towers.Count == 0) yield break;

        foreach (TowerData tower in loadedData.towers)
        {
            GameObject tempTower = Instantiate(GetTowerPrefab(tower.towerName));
            TowerSlotHolder slot = GetSlot(tower.slotId);
            slot.PlaceTower(tempTower.GetComponent<TowerBase>());
            tempTower.GetComponent<TowerBase>().SetIsSettled(true, slot);
        }
    }


    public GameObject GetTowerPrefab(string name)
    {
        foreach (var tower in towersPrefabs)
        {
            if (tower.GetComponent<TowerBase>().config.towerName == name)
            {
                return tower;
            }
        }

        return null;
    }

    public TowerSlotHolder GetSlot(int id)
    {
        foreach (TowerSlotHolder slot in _towerSlotHolders)
        {
            if (slot.slotId == id)
            {
                return slot;
            }
        }

        return null;
    }

    public int GetCurrentPrice(TowerConfig config)
    {
        foreach (var upgrade in loadedData.upgrades)
        {
            if (upgrade.towerName == config.towerName)
            {
                return upgrade.price;
            }
        }

        return 0;
    }
    public void CollectBooster()
    {
        boosterPanel.FillBooster(1);
    }

    public void TriggeredDieEvent()
    {
        OnPlayerLoseWar?.Invoke();
    }

    void GameStartToStop()
    {
        IsGameStart = false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                
                UnityEditor.EditorApplication.isPlaying = false;
        #else
               
                Application.Quit();
        #endif
    }

 

}
