using UnityEngine;

public class HealthController : MonoBehaviour {
    private const int MaxHealth = 5;
    private int _currentHealth;

    [SerializeField]
    private Animator _playerAnimator;

    [SerializeField]
    private AudioSource _audioSource;
    
    [SerializeField]
    private AudioClip DamageClip;
    
    [SerializeField]
    private AudioClip DieClip;

    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int Death = Animator.StringToHash("Death");
    private AudioSource _lifeMusic;


    private void Start() {
        GameEvents.Damaged.AddListener(TakeDamage);
        _currentHealth = MaxHealth;
        _lifeMusic = GetComponent<AudioSource>();
    }

    private void TakeDamage() {
        _currentHealth -= 1;
        _audioSource.clip = DamageClip;
        _audioSource.Play();
        
        _playerAnimator.SetTrigger(Hurt);
        Debug.Log("current hp: " + _currentHealth);

        if (_currentHealth == 0) {
            Invoke("Die", 0f);
        }
    }

    private void Die() {
        GameEvents.Die();
        _playerAnimator.SetTrigger(Death);
        _lifeMusic.Stop();
        _audioSource.clip = DieClip;
        _audioSource.Play();
    }
}