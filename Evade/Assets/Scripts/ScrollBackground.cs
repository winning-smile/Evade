using UnityEngine;

public class Scroll : MonoBehaviour {
    private static float _speed = 0.2f;
    private Vector2 startPosition;

    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        float newPosition = Mathf.Repeat(Time.time * _speed, 3.84f);
        transform.position = startPosition + Vector2.right * newPosition;
    }
}