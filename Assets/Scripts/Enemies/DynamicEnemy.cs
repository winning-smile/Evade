using UnityEngine;

public class DynamicEnemy : Enemy {
    private const float Speed = 0.05f;

    private readonly Player _inst = Player.Instance;

    void FixedUpdate() {
        var direction = _inst.transform.position - transform.position;
        var targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = new Quaternion(0f, 0f, targetRotation.z, targetRotation.w);

        transform.position = Vector2.MoveTowards(transform.position, _inst.transform.position, Speed);
    }
}