using System;
using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeButtonManager> allUpgrades = new List<UpgradeButtonManager>();
    private DatabaseReference databaseReference;
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }

  
    private void InitializeFirebase()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Firebase initialized");
     GameManager.Instance.AskToLoadGame();
    }
  
    public void SaveGame()
    {
        var saveData = new SaveData
        {
            gold = GameManager.Instance.playerGold,
            currentScore = GameManager.Instance.playerScore,
             currentSpawnInterval =GameObject.FindObjectOfType<EnemyManager>().spawnInterval
        };

        saveData.boosterValue = FindObjectOfType<BoosterPanel>().GetCurrentBooster();
        var towers = FindObjectsOfType<TowerBase>();
        if (towers == null || towers.Length == 0)
        {
            Debug.LogWarning("No towers found to save.");
        }
        else
        {
            foreach (var tower in towers)
            {
                var towerData = new TowerData
                {
                    towerName = tower.config.towerName,
                    slotId = tower.GetParentId(),
                };
                saveData.towers.Add(towerData);
            }
        }
        foreach (var button in allUpgrades)
        {
            var upgradeData = new UpgradeData()
            {
                towerName = button.GetConfig().towerName, 
                price = button.price
            };
            saveData.upgrades.Add(upgradeData);
        }

        string json = JsonUtility.ToJson(saveData);
        databaseReference.Child("saveData").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Game saved successfully.");
            }
            else
            {
                Debug.LogError("Failed to save game: " + task.Exception);
            }
        });
    }


    public void LoadGame()
    {
        databaseReference.Child("saveData").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    var saveData = JsonUtility.FromJson<SaveData>(snapshot.GetRawJsonValue());
                    GameManager.Instance.GetLoadedData(saveData);        
                 
                 
                    Debug.Log("Game loaded successfully.");
                }
                else
                {
                    Debug.Log("No saved game data found.");
                }
            }
            else
            {
                Debug.LogError("Failed to load game: " + task.Exception);
            }
        });
    }

  
}