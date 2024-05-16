using UnityEngine;

public static class GameCondition {
    private static bool _isPaused;

    public static bool IsPaused => _isPaused;

    public static void SetPause(bool flag) {
        _isPaused = flag;
        Debug.Log("POOP");
    }
}