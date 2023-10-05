using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public WeaponTypes weaponType;
    [SerializeField] private GameObject _normalBulletPrefab;
    [SerializeField] private GameObject _explodingBulletPrefab;
    [SerializeField] private Transform firePoint;

    private Bullet _currentWeaponType;
    private float shotsInterval;
    private Player _p;

    private void Awake()
    {
        _p = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (GetWeaponType() == null) return;
        
        if (shotsInterval <= 0)
        {
            GameObject bullet = ObjectPool.Instance.PoolObject(GetWeaponType(), firePoint.position);
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().ChangeDirection(firePoint.up);
            
            shotsInterval = 1f / _p.fireRate; // adds interval between shots,, calculated from fire rate
        }
        else
        {
            shotsInterval -= Time.deltaTime;
        }
    }

    private GameObject GetWeaponType()
    {
        return weaponType switch
        {
            WeaponTypes.none => null,
            WeaponTypes.normal => _normalBulletPrefab,
            WeaponTypes.exploding => _explodingBulletPrefab,
            _ => null
        };
    }

    public enum WeaponTypes
    {
        none,
        normal,
        exploding,
    }
}