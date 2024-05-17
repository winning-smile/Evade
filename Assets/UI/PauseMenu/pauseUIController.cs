using UnityEngine;
using UnityEngine.UIElements;

public class PauseUIController : MonoBehaviour {
    public PauseUI _PauseUI;

    private void OnValidate() {
        if (!_PauseUI) {
            _PauseUI = GetComponentInChildren<PauseUI>();
        }
    }

    private void OnEnable() {
        GameEvents.Paused.AddListener(ShowUI);
        GameEvents.Unpaused.AddListener(HideUI);
    }

    private void ShowUI() {
        var menuUIDocument = _PauseUI.GetComponent<UIDocument>();
        menuUIDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void HideUI() {
        var menuUIDocument = _PauseUI.GetComponent<UIDocument>();
        menuUIDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
}