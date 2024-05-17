using UnityEngine.Events;

public static class GameEvents {
    public static UnityEvent Damaged = new UnityEvent();
    public static UnityEvent DiffRaised = new UnityEvent();
    public static UnityEvent Paused = new UnityEvent();
    public static UnityEvent Unpaused = new UnityEvent();
    public static UnityEvent Killed = new UnityEvent();

    private static bool _isPaused;

    public static void ApplyDamage() {
        Damaged.Invoke();
    }

    public static void RaiseDiff() {
        DiffRaised.Invoke();
    }

    public static void Die() {
        Killed.Invoke();
    }

    public static void SwitchPause() {
        _isPaused = !_isPaused;

        if (!_isPaused) {
            Unpaused.Invoke();
        } else {
            Paused.Invoke();
        }
    }
}