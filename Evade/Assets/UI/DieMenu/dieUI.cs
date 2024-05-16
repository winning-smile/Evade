using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class dieUI : UIElement
{
    private Button _retryButton;
    private Button _menuButton;
    private Button _quitButton;

    private void OnEnable() {
        _root.style.display = DisplayStyle.None;
        InitButtons();
    }

    private void InitButtons() {
        _retryButton = _root.Q("RetryButton") as Button;
        _menuButton = _root.Q("MenuButton") as Button;
        _quitButton = _root.Q("ExitButton") as Button;

        _retryButton?.RegisterCallback<ClickEvent>(OnRetryButtonClicked);
        _quitButton?.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
        _menuButton?.RegisterCallback<ClickEvent>(OnMenuButtonClicked);
    }

    private void OnRetryButtonClicked(ClickEvent evt) {
        SceneManager.LoadScene("Game");
    }
    
    private void OnMenuButtonClicked(ClickEvent evt) {
        SceneManager.LoadScene("Menu");
    }

    private void OnQuitButtonClicked(ClickEvent evt) {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
