
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public static event Action ShowUpgradeScreen;
    public static event Action<int, int> UpdateExperienceBar;

    private int _exp;
    private int _requiredExp;

    private Player _p;
    
    private void Awake()
    {
        _p = GetComponent<Player>();
        CalculateRequiredExp();
    }
    
    
    private void Update()
    {
        CollectOrb();
    }
    
    private void CalculateRequiredExp()
    {
        // Assuming a logarithmic progression for required experience
        _requiredExp = Mathf.FloorToInt(10 * Mathf.Log(_p.level + 1, 2));
    }
    
    public void GainExperience()
    {
        _exp ++;

        if (_exp >= _requiredExp)
        {
            LevelUp();
        }
        
        UpdateExperienceBar?.Invoke(_exp, _requiredExp);
    }
    
    private void LevelUp()
    {
        _p.level++;
        _exp = 0;
        CalculateRequiredExp();
        ShowUpgradeScreen?.Invoke();
        Time.timeScale = 0;
    }


    void CollectOrb()
    {
        GameObject[] orbs = GetNearbyOrbs();

        foreach (GameObject orb in orbs)
        {
            float distance = Vector2.Distance(transform.position, orb.transform.position);

            if (distance <= _p.pickUpRange)
            {
                orb.transform.position = Vector2.Lerp(orb.transform.position, transform.position, 15f * Time.deltaTime);
            }

            if (distance <= _p.range)
            {
                orb.SetActive(false);
                GainExperience();
            }
        }
    }
    
    GameObject[] GetNearbyOrbs()
    {
        return GameObject.FindGameObjectsWithTag("Experience");
    }

    
}
