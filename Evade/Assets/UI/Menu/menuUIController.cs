using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MenuUIController : MonoBehaviour {
    [FormerlySerializedAs("_menuUI")]
    public MenuUI menuUI;

    public AudioSource AudioSource;
    public AudioClip ClickClip;
    public AudioClip OverClip;

    private UIDocument[] _documents;

    private void OnValidate() {
        if (!AudioSource) {
            AudioSource = GetComponent<AudioSource>();
        }
    }

    private void OnEnable() {
        _documents = GetComponentsInChildren<UIDocument>();

        InitButtons();
    }

    private void InitButtons() {
        var buttons = _documents.SelectMany(item => item.rootVisualElement.Query<Button>().ToList());

        foreach (var button in buttons) {
            button.RegisterCallback<ClickEvent>(OnButtonClicked);
            button.RegisterCallback<MouseOverEvent>(evt => OnMouseOver());
        }
    }

    private void OnButtonClicked(ClickEvent evt) {
        if (AudioSource.isPlaying) {
            return;
        }

        AudioSource.clip = ClickClip;
        AudioSource.Play();
    }

    private void OnMouseOver() {
        if (AudioSource.isPlaying) {
            return;
        }

        AudioSource.clip = OverClip;
        AudioSource.Play();
    }
}