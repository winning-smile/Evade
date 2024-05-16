using System;
using UnityEngine;

public class LinearEnemy : Enemy {
    private float _speed = 5f;
        
    private void Update() {
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
    }
}