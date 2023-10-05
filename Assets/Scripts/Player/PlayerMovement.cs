using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;

    private Vector3 _mousePosition;
    private Vector3 aimDirection;
    private float aimAngle;

    private Player _p;

    private void Awake()
    {
        _p = GetComponent<Player>();
    }

    void Update()
    {
        MousePosition();
       
    }

    private void FixedUpdate()
    {
        AimInput();
        Move();
    }

    private void MousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void AimInput()
    {
        aimDirection = (_mousePosition - transform.position).normalized;
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        aimTransform.eulerAngles = new Vector3(0, 0, aimAngle);
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized;
        float moveX = transform.position.x + direction.x * _p.speed * Time.deltaTime;
        float moveY = transform.position.y + direction.y * _p.speed * Time.deltaTime;

        transform.position = new Vector3(moveX, moveY, transform.position.z);
    }
}