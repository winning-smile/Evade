using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private void Awake() {

        if (Instance == null) {
            Instance = this;
            return;
        }
        
        Destroy(this);
    }
}
