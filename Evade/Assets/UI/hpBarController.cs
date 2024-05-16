using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class hpBarController : MonoBehaviour {
    public hpBarUI _HpBarUI;
    
    private void OnValidate() {
        if (!_HpBarUI) {
            _HpBarUI = GetComponentInChildren<hpBarUI>();
        }
    }
    private void OnEnable() {
    }
    
}
