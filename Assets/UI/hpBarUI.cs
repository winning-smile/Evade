using System.Linq;
using UnityEngine.UIElements;

public class HpBarUI : UIElement {
    private VisualElement _hpContainer;
    private VisualElement[] _hearts;
    private int _counter;

    private void OnEnable() {
        InitContainer();
    }

    private void InitContainer() {
        _hpContainer = _root.Q("HPContainer");
        _hearts = _hpContainer.Children().ToArray();
        _counter = _hearts.Length - 1;
        GameEvents.Damaged.AddListener(RemoveHp);
    }

    private void RemoveHp() {
        _hearts[_counter].style.display = DisplayStyle.None;
        _counter--;
    }
}