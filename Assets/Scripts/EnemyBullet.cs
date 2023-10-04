using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private float _bulletRange = 1;
    

    private PlayerHealth _player;
    
    private Vector3 initialPosition;
    private Vector2 direction = Vector2.up;

    private void Start()
    {
        initialPosition = transform.position;
        _player = GetPlayer();
    }

    private void Update()
    {
        MoveBullet();
        CheckCollision();
    }

    void MoveBullet()
    {
        transform.Translate(direction * _speed * Time.deltaTime);

        if (Vector3.Distance(initialPosition, transform.position) >= _maxDistance)
        {
            DisableBullet();
        }
    }

    void CheckCollision()
    {
        
        if (Vector3.Distance(transform.position, _player.transform.position) < _bulletRange)
        {
            DisableBullet();
            _player.DecreaseHealth();
        }
    }

    public void ChangeDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized; 
    }

    private void DisableBullet()
    {
        //add particle
        
        gameObject.SetActive(false);
        
    }
    
    public PlayerHealth GetPlayer()
    {
        return FindObjectOfType<PlayerHealth>();
    }


}
