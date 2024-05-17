using UnityEngine;
using UnityEngine.Serialization;

public class HpBarController : MonoBehaviour {
    [FormerlySerializedAs("_HpBarUI")]
    public HpBarUI hpBarUI;

    private void OnValidate() {
        if (!hpBarUI) {
            hpBarUI = GetComponentInChildren<HpBarUI>();
        }
    }
}