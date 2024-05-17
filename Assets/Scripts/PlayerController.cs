using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private const float MoveSpeed = 3f;
    private const float JumpHeight = 8f;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private bool _isSPressed;

    [SerializeField]
    private Transform _groundCheck;

    [SerializeField]
    private Transform _groundCheck_l;

    [SerializeField]
    private Transform _groundCheck_r;

    [SerializeField]
    private Transform _wallCheck_r;

    [SerializeField]
    private Transform _wallCheck_l;

    [SerializeField]
    private LayerMask _groundLayerMask;

    [SerializeField]
    private LayerMask _baseGroundLayer;

    [SerializeField]
    private LayerMask _playerLayerMask;

    [SerializeField]
    private Animator _animator;
    
    [SerializeField]
    private AudioSource _audioSource;
    
    [SerializeField]
    private AudioClip StartClip;
    

    private static readonly int AnimState = Animator.StringToHash("AnimState");
    private static readonly int Grounded = Animator.StringToHash("Grounded");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int AirSpeedY = Animator.StringToHash("AirSpeedY");

    private int _playerLayer;
    private int _groundLayer;
    private static readonly int WallSlide = Animator.StringToHash("WallSlide");

    void Start() {
        Cursor.visible = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerLayer = LayerMask.NameToLayer("Player");
        _groundLayer = LayerMask.NameToLayer("Ground");
        GameEvents.Paused.AddListener(SetPause);
        GameEvents.Unpaused.AddListener(UnsetPause);
        GameEvents.Killed.AddListener(DisableControl);
        
        _audioSource.clip = StartClip;
        _audioSource.Play();
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
            if (Physics2D.Linecast(transform.position, _wallCheck_r.position, _groundLayerMask)) {
                _animator.SetBool(WallSlide, true);
                _spriteRenderer.flipX = false;
            }

            _animator.SetInteger(AnimState, 1);
            _rigidbody.velocity = new Vector2(MoveSpeed, _rigidbody.velocity.y);
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey("a")) {
            if (Physics2D.Linecast(transform.position, _wallCheck_l.position, _groundLayerMask)) {
                _animator.SetBool(WallSlide, true);
                _spriteRenderer.flipX = true;
            }

            _animator.SetInteger(AnimState, 1);
            _rigidbody.velocity = new Vector2(-MoveSpeed, _rigidbody.velocity.y);
            _spriteRenderer.flipX = true;
        }

        if (Input.GetKey("w") && _isGrounded) {
            _animator.SetTrigger(Jump);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpHeight);
        }

        if (_rigidbody.velocity.y > 0) {
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, true);
        } else if (_rigidbody.velocity.y < 0 && !_isSPressed) {
            _animator.SetFloat(AirSpeedY, -1);
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, false);
        }
    }

    private IEnumerator DownCooldown() {
        yield return new WaitForSeconds(0.5f);
        _isSPressed = false;
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
        if (Input.GetKeyDown("s") && _isGrounded) {
            _animator.SetFloat(AirSpeedY, -1);
            Physics2D.IgnoreLayerCollision(_playerLayer, _groundLayer, true);
            _isSPressed = true;
            StartCoroutine(DownCooldown());
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameEvents.SwitchPause();
        }
    }
}