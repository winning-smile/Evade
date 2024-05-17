using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class DieUIController : MonoBehaviour {
    [FormerlySerializedAs("_dieUI")]
    public DieUI dieUI;
    
    [SerializeField]
    private AudioSource _audioSource;
    
    [SerializeField]
    private AudioClip DieMusic;

    private void OnValidate() {
        if (!dieUI) {
            dieUI = GetComponentInChildren<DieUI>();
        }
    }

    private void OnEnable() {
        GameEvents.Killed.AddListener(ShowUI);
    }

    private void ShowUI() {
        var menuUIDocument = dieUI.GetComponent<UIDocument>();
        menuUIDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        _audioSource.clip = DieMusic;
        _audioSource.Play();
    }
}