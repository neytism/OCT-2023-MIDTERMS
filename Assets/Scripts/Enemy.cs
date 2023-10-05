using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    public float size;
    public bool isAlive;
    public GameObject healthBarHolder;
    public Image healthBar;
    
    public GameObject expPrefab;

    public int maxHealth;
    
    public int health;
    
    public float speed;
    
    public SpriteRenderer spriteRenderer;
    
    public Color colorHurt;
    
    public Color colorNormal;
    public abstract void Move();
    public abstract void DecreaseHealth();
    public abstract void Die();
    public abstract void Drop();
    
    public abstract Transform GetPlayerTransform();

    public abstract void UpdateHealthBar(float currentVal, float maxVal);

    public IEnumerator ColorTick()
    {
        spriteRenderer.color = colorHurt;
        yield return new WaitForSeconds(.075f);
        spriteRenderer.color = colorNormal;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        Gizmos.DrawWireSphere(transform.position, size);
        
    }
}
