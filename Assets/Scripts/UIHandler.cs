using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Image expBar;
    public TextMeshProUGUI healthText;
    
    private void OnEnable()
    {
        PlayerExperience.UpdateExperienceBar += UpdateExpBar;
        PlayerHealth.UpdateHealthCount += UpdateHealthCount;
    }

    private void OnDisable()
    {
        PlayerExperience.UpdateExperienceBar -= UpdateExpBar;
        PlayerHealth.UpdateHealthCount -= UpdateHealthCount;
    }


    private void UpdateExpBar(int currentVal, int maxVal)
    {
        expBar.fillAmount = (float)currentVal / maxVal;
    }

    private void UpdateHealthCount(int value)
    {
        healthText.text = value.ToString();
    }
    
    public void Restart()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
