using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject weaponPanel;
    private PlayerShoot _p;

    private void Awake()
    {
        _p = GetComponent<PlayerShoot>();
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void SetWeaponToNormal()
    {
        _p.weaponType = PlayerShoot.WeaponTypes.normal;
        HideWeaponPanel();
    }
    
    public void SetWeaponToExploding()
    {
        _p.weaponType = PlayerShoot.WeaponTypes.exploding;
        HideWeaponPanel();
    }

    private void HideWeaponPanel()
    {
        weaponPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
