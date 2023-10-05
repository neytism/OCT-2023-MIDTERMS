using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    public bool enableDebug = false;
    
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxDistance = 10f;
    
    [SerializeField] private TrailRenderer _tr;
    [SerializeField] private GameObject _particle;

    private Vector3 initialPosition;
    private Vector2 direction = Vector2.up;

    private void OnEnable()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        MoveBullet();
        CheckCollision();
    }

    public override void MoveBullet()
    {
        transform.Translate(direction * _speed * Time.deltaTime);

        if (Vector3.Distance(initialPosition, transform.position) >= _maxDistance)
        {
            DisableBullet();
        }
    }

    public override void CheckCollision()
    {
        Enemy[] nearbyTargets = GetNearbyTargets();

        foreach (Enemy target in nearbyTargets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < target.size)
            {
                if(!target.isAlive) return;
                DisableBullet();
                target.DecreaseHealth();
            }
        }
    }

    public override Enemy[] GetNearbyTargets()
    {
        return FindObjectsOfType<Enemy>();
    }
    
    public override void ChangeDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; 
    }

    public override void DisableBullet()
    {
        GameObject particle = ObjectPool.Instance.PoolObject(_particle, transform.position);
        particle.SetActive(true);
        
        gameObject.SetActive(false);
        _tr.Clear();
        
    }


}