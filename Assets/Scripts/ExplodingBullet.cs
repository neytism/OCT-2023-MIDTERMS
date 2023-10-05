using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBullet : Bullet
{
    public bool enableDebug = false;
    
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxDistance = 5f;
    [SerializeField] private float _explodeRadius = 2f;
    [SerializeField] private float _explodeDelay = 2f;
    
    
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
        if (Vector3.Distance(initialPosition, transform.position) >= _maxDistance)
        {
            StartCoroutine(ExplodeAfterDelay());
            return;
        }
        
        transform.Translate(direction * _speed * Time.deltaTime);
        
    }

    public override void CheckCollision()
    {
        Enemy[] nearbyTargets = GetNearbyTargets();

        foreach (Enemy target in nearbyTargets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < target.size)
            {
                if(!target.isAlive) return;
                Explode();
                target.DecreaseHealth();
            }
        }
    }

    private void Explode()
    {
        Enemy[] nearbyTargets = GetNearbyTargets();
        
        foreach (Enemy target in nearbyTargets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < _explodeRadius)
            {
                if(target.isAlive) target.DecreaseHealth();
            }
        }
        
        DisableBullet();
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

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(_explodeDelay); // You can adjust the delay before exploding
        Explode();
    }

}