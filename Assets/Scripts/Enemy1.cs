using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    [SerializeField] private Animator _anim;
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
    }

    public override void Move()
    {
        if (!isAlive) return;
        Vector2 direction = (GetPlayerTransform().position - transform.position).normalized;
        
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    public override void DecreaseHealth()
    {
        health--;
        StartCoroutine(ColorTick());
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

    IEnumerator DeathSequence()
    {
        healthBarHolder.SetActive(false);
        _anim.SetBool("IsDead", !isAlive);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
