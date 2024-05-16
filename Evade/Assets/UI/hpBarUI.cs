using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class hpBarUI : UIElement {
    private VisualElement _hpContainer;
    private VisualElement[] _hearts;
    private int counter;
    private void OnEnable() {
        InitContainer();
    }
    
    private void InitContainer() {
        _hpContainer = _root.Q("HPContainer");
        _hearts = _hpContainer.Children().ToArray();
        counter = _hearts.Length - 1;
        GameEvents.Damaged.AddListener(removeHp);
    }

    private void removeHp() {
        _hearts[counter].style.display = DisplayStyle.None;
        counter--;
    }
}
