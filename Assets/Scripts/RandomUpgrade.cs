using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomUpgrade : MonoBehaviour
{
    [SerializeField] private Transform upgradeHolder;

    private List<Transform> upgradesButtons;

    private int activeButtons = 0;

    private void Start()
    {
        InitializeUpgrades();
    }

    private void OnEnable()
    {
        PlayerExperience.ShowUpgradeScreen += ReplaceUpgradesOption;
    }

    private void OnDisable()
    {
        PlayerExperience.ShowUpgradeScreen -= ReplaceUpgradesOption;
    }

    private void InitializeUpgrades()
    {
        upgradesButtons = new List<Transform>();

        foreach (Transform child in upgradeHolder)
        {
            upgradesButtons.Add(child);
        }
    }
    

    private void ReplaceUpgradesOption()
    {
        
        
        activeButtons = 0;
        foreach (Transform btn in upgradesButtons)
        {
            btn.gameObject.SetActive(false);
        }

        while (activeButtons < 3)
        {
            int rand = Random.Range(0, upgradesButtons.Count);

            if (!upgradesButtons[rand].gameObject.activeInHierarchy)
            {
                upgradesButtons[rand].gameObject.SetActive(true);
                activeButtons++;
            }
        }
    }
}