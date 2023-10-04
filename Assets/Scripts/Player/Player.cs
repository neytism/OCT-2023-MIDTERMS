using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 1;
    public float speed;
    public float range = 1f;
    public float fireRate = 1f;
    public float pickUpRange = 3f;

    public int health;
    public int maxHealth;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        Gizmos.DrawWireSphere(transform.position, range);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}