using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class pauseUI : UIElement
{
    private Button _resumeButton;
    private Button _menuButton;
    private Button _quitButton;
    
    private void OnEnable() {
        _root.style.display = DisplayStyle.None;
        InitButtons();
    }
    
    private void InitButtons() {
        _resumeButton = _root.Q("ContinueButton") as Button;
        _menuButton = _root.Q("MenuButton") as Button;
        _quitButton = _root.Q("ExitButton") as Button;

        _resumeButton?.RegisterCallback<ClickEvent>(OnResumeButtonClicked);
        _menuButton?.RegisterCallback<ClickEvent>(OnMenuButtonClicked);
        _quitButton?.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
    }
    
    private void OnResumeButtonClicked(ClickEvent evt) {
        GameEvents.SwitchPause();
    }
    
    private void OnQuitButtonClicked(ClickEvent evt) {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    private void OnMenuButtonClicked(ClickEvent evt) {
        SceneManager.LoadScene("Menu");
    }
}
