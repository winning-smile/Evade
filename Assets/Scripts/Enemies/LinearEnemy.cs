using UnityEngine;

public class LinearEnemy : Enemy {
    private const float Speed = 5f;

    private void Update() {
        transform.Translate(MoveDirection * Speed * Time.deltaTime);
    }
}