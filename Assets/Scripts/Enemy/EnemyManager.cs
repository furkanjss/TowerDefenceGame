using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyConfig[] enemyConfigs;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform playerTower;
    [SerializeField] float spawnInterval = 5f;
    private float spawnTimer;
    public float CurrentDifficulty { get; set; }
    public float SpawnInterval { get; set; }
    private float initialScale;
    private void Start()
    {
        SpawnInterval = spawnInterval;
        initialScale = transform.localScale.x;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemy();
            ScaleUpEffect();
            spawnTimer = SpawnInterval;
            SpawnInterval *= 0.99f; 
        }
    }

    private void SpawnEnemy()
    {
        var enemyConfig = SelectEnemyConfig(); 
        var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<EnemyBase>();
        enemy.Initialize(enemyConfig, playerTower); 
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

   
}