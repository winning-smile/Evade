using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float _moveSpeed = 3f;
    private float _jumpHeight = 8f;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    bool _isGrounded;

    [SerializeField]
    private Transform _groundCheck;

    [SerializeField]
    private Transform _groundCheck_l;

    [SerializeField]
    private Transform _groundCheck_r;

    [SerializeField]
    private LayerMask _groundLayerMask;

    [SerializeField]
    private LayerMask _baseGroundLayer;

    [SerializeField]
    private LayerMask _playerLayerMask;

    [SerializeField]
    private Animator _animator;

    private static readonly int AnimState = Animator.StringToHash("AnimState");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int AirSpeedY = Animator.StringToHash("AirSpeedY");

    private int _playerLayer;
    private int _groundLayer;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerLayer = LayerMask.NameToLayer("Player");
        _groundLayer = LayerMask.NameToLayer("Ground");
        GameEvents.Paused.AddListener(SetPause);
        GameEvents.Unpaused.AddListener(UnsetPause);
        GameEvents.Killed.AddListener(DisableControl);
    }

    void FixedUpdate() {
        _animator.SetInteger(AnimState, 0);
        if (Physics2D.Linecast(transform.position, _groundCheck.position, _groundLayerMask)
            || Physics2D.Linecast(transform.position, _groundCheck_l.position, _groundLayerMask)
            || Physics2D.Linecast(transform.position, _groundCheck_r.position, _groundLayerMask)
            || Physics2D.Linecast(transform.position, _groundCheck.position, _baseGroundLayer)) {
            _isGrounded = true;
            _animator.SetBool(Grounded, true);
            _animator.SetFloat(AirSpeedY, 1);
        } else {
            _isGrounded = false;
            _animator.SetBool(Grounded, false);
        }

        if (Input.GetKey("d")) {
            _animator.SetInteger(AnimState, 1);
            _rigidbody.velocity = new Vector2(_moveSpeed, _rigidbody.velocity.y);
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey("a")) {
            _animator.SetInteger(AnimState, 1);
            _rigidbody.velocity = new Vector2(-_moveSpeed, _rigidbody.velocity.y);
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKey("w") && _isGrounded) {
            _animator.SetTrigger(Jump);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpHeight);
        }

        if (Input.GetKeyDown("s") && _isGrounded) {
            Debug.Log("GOOOOOOOOOOOOOOOL");
            _animator.SetFloat(AirSpeedY, -1);
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, true);
        }

        if (_rigidbody.velocity.y > 0) {
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, true);
        } else if (_rigidbody.velocity.y < 0 && !Input.GetKeyDown("s")) {
            _animator.SetFloat(AirSpeedY, -1);
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false);
        }
    }

    private void SetPause() {
        Time.timeScale = 0;
    }

    private void DisableControl() {
        this.enabled = false;
    }

    private void UnsetPause() {
        Time.timeScale = 1;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameEvents.SwitchPause();
        }
    }
}