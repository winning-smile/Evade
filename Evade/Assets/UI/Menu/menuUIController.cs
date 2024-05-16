using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuUIController : MonoBehaviour
{
    public menuUI _menuUI;
        
    private void OnValidate() {
        if (!_menuUI) {
            _menuUI = GetComponentInChildren<menuUI>();
        }
    }
}
