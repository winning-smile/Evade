using UnityEngine;

public class HealthController : MonoBehaviour {
    private const int _maxHealth = 5;
    private int _currentHealth;

    [SerializeField]
    private Animator _playerAnimator;
    
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int Death = Animator.StringToHash("Death");
    private AudioSource _lifeMusic;


    private void Start() {
        GameEvents.Damaged.AddListener(TakeDamage);
        _currentHealth = _maxHealth;
        _lifeMusic = GetComponent<AudioSource>();
    }

    private void TakeDamage() {
        _currentHealth -= 1;
        _playerAnimator.SetTrigger(Hurt);
        Debug.Log("current hp: " + _currentHealth);

        if (_currentHealth == 0) {
            Invoke("DIE", 0f);
        }
    }

    private void DIE() {
        GameEvents.Die();
        _playerAnimator.SetTrigger(Death);
        _lifeMusic.Stop();
    }
}