using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    
    [SerializeField] private float _spawnRadius = 10f;

    [SerializeField] private GameObject _enemy1;
    [SerializeField] private GameObject _enemy2;
    [SerializeField] private GameObject _enemy3;

    private int numberOfSpawned = 0;
    
    private float spawnInterval = 1.5f; 
    private int waveNumber = 1;
    
    
    
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(DelayBeforeSpawning());
    }
    
    IEnumerator DelayBeforeSpawning()   //adds delay to show mini tutorial pop up
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(GameCycle());
    }
    
    IEnumerator GameCycle()
    {
        StartCoroutine(SpawnBatch(spawnInterval, _enemy1));
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnBatch(spawnInterval, _enemy1));
        
        StartCoroutine(SpawnBatch(spawnInterval * 1.25f, _enemy2));
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnBatch(spawnInterval * 1.25f, _enemy2));
        
        StartCoroutine(SpawnBatch(spawnInterval,_enemy1, _enemy2));
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnBatch(spawnInterval, _enemy1,_enemy2));
        
        StartCoroutine(SpawnBatch(spawnInterval, _enemy1, _enemy2));
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnBatch(spawnInterval, _enemy1, _enemy2));
        
        StartCoroutine(SpawnBatch(60, _enemy3));
        yield return new WaitForSeconds(60);
        StopCoroutine(SpawnBatch(60, _enemy3));
        
        IncreaseDifficulty();
        StartCoroutine(GameCycle());
    }
    
    IEnumerator SpawnBatch(float interval, GameObject enemyspawn1)
    {
        SpawnEnemyObject(enemyspawn1);
        numberOfSpawned++;
        
        yield return new WaitForSeconds(interval);

        StartCoroutine(SpawnBatch( interval,enemyspawn1));  //loops
        
    }
    
    IEnumerator SpawnBatch(float interval, GameObject enemyspawn1, GameObject enemyspawn2)
    {
        
        GameObject[] list = new GameObject[3];
        list[0]= enemyspawn1;
        list[1]= enemyspawn2;
        
        SpawnEnemyObject(list[(int)Random.Range(0f,2f)]);
        numberOfSpawned++;

        yield return new WaitForSeconds(interval);
        
        StartCoroutine(SpawnBatch( interval,enemyspawn1,enemyspawn2));  //loops
        
    }


    IEnumerator SpawnBatch(float interval, GameObject enemyspawn1, GameObject enemyspawn2, GameObject enemyspawn3)
    {
       
        GameObject[] list = new GameObject[3];
        list[0]= enemyspawn1;
        list[1]= enemyspawn2;
        list[2]= enemyspawn3;
        
        SpawnEnemyObject(list[(int)Random.Range(0f,3f)]);
        numberOfSpawned++;
        
        yield return new WaitForSeconds(interval);

        StartCoroutine(SpawnBatch( interval,enemyspawn1,enemyspawn2,enemyspawn3));  //loops
    }


    private void SpawnEnemyObject(GameObject gameObject)
    {
        Vector2 spawnPos = FindObjectOfType<Player>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * _spawnRadius;
        
        GameObject enemy = ObjectPool.Instance.PoolObject(gameObject, spawnPos);
        enemy.SetActive(true);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        
    }
    
    void IncreaseDifficulty()
    {
        waveNumber++;
        spawnInterval *= Mathf.Log(waveNumber, 2);
    }
}