using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    public int totalSpawnEnemy;
    public EnemyConfig[] enemyConfigs;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform playerTower;
   public float spawnInterval = 5f;
   public const float minInterval = .6f;
    private float spawnTimer;
    
    private float initialScale;
    private GameManager _gameManager;
    private void Start()
    {
      
        initialScale = transform.localScale.x;
        _gameManager=GameManager.Instance;
    }

    private void Update()
    {
        if(!_gameManager.IsGameStart)return;
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            ScaleUpEffect();
            spawnTimer = spawnInterval;
            if (spawnInterval>minInterval)
            {
                spawnInterval *= 0.99f; 

            }
        }
    }

    private void SpawnEnemy()
    {
        var enemyConfig = SelectEnemyConfig(); 
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBase>();
        enemy.Initialize(enemyConfig, playerTower); 
    }

    private void OnEnable()
    {
        GameManager.OnGameDataLoad += GetSpawnInterval;
    }
 private void OnDisable()
    {
        GameManager.OnGameDataLoad -= GetSpawnInterval;
    }

    void ScaleUpEffect()
    {
        transform.DOScale(initialScale + .2f,.1f).OnComplete(() =>
        {
            transform.DOScale(initialScale, .1f);
        });
    }
    private EnemyConfig SelectEnemyConfig()
    {
      
        return enemyConfigs[Random.Range(0, enemyConfigs.Length)];
    }

    public void GetSpawnInterval()
    {
        spawnInterval = GameManager.Instance.loadedData.currentSpawnInterval;
    }
   
}