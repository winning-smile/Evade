using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIElement : MonoBehaviour {
    protected VisualElement _root;

    protected virtual void Awake() {
        _root = GetComponent<UIDocument>().rootVisualElement;
    }
}