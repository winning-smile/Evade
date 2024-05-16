using System;
using UnityEngine;

public class DynamicEnemy : Enemy {
    private float _speed = 0.05f;
    
    private Player inst = Player.Instance;
    
    void FixedUpdate() {
        var direction = inst.transform.position - transform.position;
        var targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = new Quaternion(0f, 0f, targetRotation.z, targetRotation.w);
        
        transform.position = Vector2.MoveTowards(transform.position, inst.transform.position, _speed);
    }
}