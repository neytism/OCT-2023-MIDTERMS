using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private WeaponTypes _weaponType;
    [SerializeField] private GameObject _normalBulletPrefab;
    [SerializeField] private GameObject _explodingBulletPrefab;
    [SerializeField] private Transform firePoint;

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
        if (shotsInterval <= 0)
        {
            GameObject bullet;

            if (_weaponType == WeaponTypes.normal)
            {
                bullet = ObjectPool.Instance.PoolObject(_normalBulletPrefab, firePoint.position);
                bullet.SetActive(true);
                if (bullet != null) bullet.GetComponent<NormalBullet>().ChangeDirection(firePoint.up);
            }
            else
            {
                bullet = ObjectPool.Instance.PoolObject(_explodingBulletPrefab, firePoint.position);
                bullet.SetActive(true);
                if (bullet != null) bullet.GetComponent<ExplodingBullet>().ChangeDirection(firePoint.up);
            }
            
            

            shotsInterval = 1f / _p.fireRate; // adds interval between shots,, calculated from fire rate
        }
        else
        {
            shotsInterval -= Time.deltaTime;
        }
    }
    
    public enum WeaponTypes
    {
        normal,
        exploding,
    }
}