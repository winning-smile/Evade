using UnityEngine;
using UnityEngine.UIElements;

public class dieUIController : MonoBehaviour
{
    public dieUI _dieUI;
        
    private void OnValidate() {
        if (!_dieUI) {
            _dieUI = GetComponentInChildren<dieUI>();
        }
    }

    private void OnEnable() {
        GameEvents.Killed.AddListener(ShowUI);
    }

    private void ShowUI() {
        var menuUIDocument = _dieUI.GetComponent<UIDocument>();
        menuUIDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}