using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    [SerializeField] private Animator _anim;
    [SerializeField] private float shotsInterval;
    [SerializeField] private GameObject _enemyBulletPrefab;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private float _minDistance;
    
    private Transform _player;

    private void Start()
    {
        _player = GetPlayerTransform();
    }

    private void OnEnable()
    {
        
        health = maxHealth;
        isAlive = true;
        _anim.SetBool("IsDead", !isAlive);
        
        UpdateHealthBar((float)health, (float)maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    public override void Move()
    {
        if (!isAlive) return;
        
        Vector2 direction = (_player.position - transform.position).normalized;

        if (Distance(transform.position, _player.position) > _minDistance)
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

    }
    
    private void Fire()
    {
        if (shotsInterval <= 0)
        {
            GameObject bullet = ObjectPool.Instance.PoolObject(_enemyBulletPrefab, transform.position);
            
            bullet.SetActive(true);

            shotsInterval = 1f / _fireRate; // adds interval between shots,, calculated from fire rate
        }
        else
        {
            shotsInterval -= Time.deltaTime;
        }
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
        isAlive = false;
        StartCoroutine(DeathSequence());
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
    
    IEnumerator DeathSequence()
    {
        healthBarHolder.SetActive(false);
        _anim.SetBool("IsDead", !isAlive);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    
}