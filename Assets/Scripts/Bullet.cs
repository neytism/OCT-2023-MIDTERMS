using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public abstract void MoveBullet();
    public abstract void CheckCollision();
    public abstract Enemy[] GetNearbyTargets();
    
    public abstract void ChangeDirection(Vector2 newDirection);
    
    public abstract void DisableBullet();


}
