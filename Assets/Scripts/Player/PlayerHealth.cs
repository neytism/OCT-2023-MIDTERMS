using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image hurtScreen;
    [SerializeField] private Color colorHurt;
    [SerializeField] private Color colorNormal;
    public static event Action<int> UpdateHealthCount;
    private Player _p;
    private bool _isInvincible;

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
        if (_isInvincible) return;
        _p.health--;
        _isInvincible = true;
        StartCoroutine(ColorTick());

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
    
    IEnumerator ColorTick()
    {
        hurtScreen.color = colorHurt;
        yield return new WaitForSeconds(.075f);
        hurtScreen.color = colorNormal;
        yield return new WaitForSeconds(1f);
        _isInvincible = false;
    }
}
