using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private SpriteRenderer _spriteRenderer;
    private float lifeTime = 4f;
    protected Vector3 _moveDirection;
    
    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (transform.position.x > 0) {
            _spriteRenderer.flipX = true;
            _moveDirection = Vector3.left;
        } else {
            _moveDirection = Vector3.right;
        }

        StartCoroutine(LifeCycle());
        GameEvents.Killed.AddListener(Refresh);
    }

    private void OnCollisionEnter2D(Collision2D body) {
        if (body.gameObject.CompareTag("Player")) {
            GameEvents.ApplyDamage();
        }

        Destroy(gameObject);
    }

    private void Refresh() {
        Destroy(this.gameObject);
    }

    private IEnumerator LifeCycle() {
        WaitForSeconds wait = new WaitForSeconds(lifeTime);
        yield return wait;
        Destroy(gameObject);
    }
    
}