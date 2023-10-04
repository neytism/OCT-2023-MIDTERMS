using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    
    private Player _p;

    private void Awake()
    {
        _p = GetComponent<Player>();
    }
    
    private void OnEnable()
    {
        PlayerExperience.ShowUpgradeScreen += ShowPanel;
    }

    private void OnDisable()
    {
        PlayerExperience.ShowUpgradeScreen -= ShowPanel;
    }

    private void ShowPanel()
    {
       panel.SetActive(true);
    }

    private void HidePanel()
    {
        panel.SetActive(false);
        UnfreezeTime();
    }

    public void IncreaseHealth()
    {
        _p.health++;

        if (_p.health > _p.maxHealth)
        {
            _p.health = _p.maxHealth;
        }

        GetComponent<PlayerHealth>().UpdateHeathUI();
        HidePanel();
    }
    
    public void IncreaseMaxHealth()
    {
        _p.maxHealth++;
        HidePanel();
    }
    
    public void IncreasePickUpRange()
    {
        _p.pickUpRange += 0.5f;
        HidePanel();
    }

    public void IncreaseSpeed()
    {
        _p.speed += 0.5f;
        HidePanel();
    }
    
    public void IncreaseFireRate()
    {
        _p.fireRate += 0.5f;
        HidePanel();
    }

    private void UnfreezeTime()
    {
        Time.timeScale = 1f;
    }
    
}
