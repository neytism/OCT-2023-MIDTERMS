using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public static event Action<int> UpdateHealthCount;
    private Player _p;

    private void Awake()
    {
        _p = GetComponent<Player>();
        _p.health = _p.maxHealth;
        UpdateHeathUI();
    }
    
    private void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        Enemy[] nearbyTargets = GetNearbyEnemies();

        foreach (Enemy target in nearbyTargets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < _p.range)
            {
                if (!target.isAlive) return;
                target.DecreaseHealth();
                DecreaseHealth();
                
            }
        }
    }

    public void DecreaseHealth()
    {
        _p.health--;

        if (_p.health <= 0)
        {
            _p.health = 0;
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }

        UpdateHeathUI();

    }

    Enemy[] GetNearbyEnemies()
    {
        return FindObjectsOfType<Enemy>();
    }

    public void UpdateHeathUI()
    {
        UpdateHealthCount?.Invoke(_p.health);
    }
}
