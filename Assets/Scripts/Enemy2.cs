using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Enemy2 : Enemy
{
    [SerializeField] private float shotsInterval;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject _enemyBulletPrefab;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _turnSpeed = 5f;
    [Range(0f, 360f)]
    [SerializeField] private float _viewAngleRange = 10f;
    
    [SerializeField] private float _fireRadius;
    [SerializeField] private float _minDistance;
    
    private Transform _player;

    public Transform body;
    public Transform canvas;
    
    private void Start()
    {
        _player = GetPlayerTransform();
    }

    private void OnEnable()
    {
        
        health = maxHealth;
        
        UpdateHealthBar((float)health, (float)maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateTowardsTarget();
    }

    public override void Move()
    {
        Vector2 direction = (_player.position - transform.position).normalized;

        if (Distance(transform.position, _player.position) > _minDistance)
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, _player.position) < _fireRadius)
        {
            if(IsTargetLocked()) return;
            Fire();
        }
        
    }
    
    private void Fire()
    {
        if (shotsInterval <= 0)
        {
            GameObject bullet = ObjectPool.Instance.PoolObject(_enemyBulletPrefab, firePoint.position);
            
            bullet.SetActive(true);
            bullet.GetComponent<EnemyBullet>().ChangeDirection(firePoint.up);

            shotsInterval = 1f / _fireRate; // adds interval between shots,, calculated from fire rate
        }
        else
        {
            shotsInterval -= Time.deltaTime;
        }
    }

    
    private void RotateTowardsTarget()
    {
        Vector2 directionToPlayer = _player.position - transform.position;
        
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 90;
        Quaternion zRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, zRotation, _turnSpeed * Time.deltaTime);

        body.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z * -1.0f);
        canvas.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z * -1.0f);
    }
    
    private bool IsTargetLocked()
    {
        Vector2 directionToPlayer = _player.transform.position - transform.position;
        float dotProduct = DotProduct(NormalizeVector(directionToPlayer), transform.up);

        return dotProduct < ConvertViewAngle(_viewAngleRange);
    }
    
    private float ConvertViewAngle(float angle)
    {
        return Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad);
    }
    
    private float DotProduct(Vector3 firstPos, Vector3 secondPos)
    {
        float xProduct = firstPos.x * secondPos.x;
        float yProduct = firstPos.y * secondPos.y;
        float zProduct = firstPos.z * secondPos.z;
        
        return xProduct + yProduct + zProduct;
    }

    private float Magnitude(Vector3 v)
    {
        return Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2) + Mathf.Pow(v.z, 2));
    }
    
    private Vector3 NormalizeVector(Vector3 v)
    {
        float mag = Magnitude(v);

        v.x /= mag;
        v.y /= mag;
        v.z /= mag;
        
        return v;
    }
    
    public override void DecreaseHealth()
    {
        health--;
        UpdateHealthBar( (float)health, (float)maxHealth);

        if (health <= 0)
        {
            Die();
        }
        
    }
    
    
    public override void UpdateHealthBar(float currentVal, float maxVal)
    {
        healthBarHolder.SetActive(currentVal != maxVal);

        healthBar.fillAmount = currentVal / maxVal;
        healthBar.color = Color.Lerp(Color.red, Color.green, currentVal / maxVal);
    }

    public override void Die()
    {
        Drop();
        gameObject.SetActive(false);
    }

    public override void Drop()
    {
        GameObject exp = ObjectPool.Instance.PoolObject(expPrefab, transform.position);
        exp.SetActive(true);
    }

    public override Transform GetPlayerTransform()
    {
        return FindObjectOfType<PlayerMovement>().transform;
    }
    
    private float Distance(Vector3 firstPos, Vector3 secondPos)
    {
        float xDifference = firstPos.x - secondPos.x;
        float yDifference = firstPos.y - secondPos.y;
        float zDifference = firstPos.z - secondPos.z;
        
        return Mathf.Sqrt(Mathf.Pow((xDifference), 2) + Mathf.Pow((yDifference), 2) + Mathf.Pow((zDifference), 2));
    }
}