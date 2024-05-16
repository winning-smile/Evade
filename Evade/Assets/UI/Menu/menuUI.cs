using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class menuUI : UIElement {
    private Button _playButton;
    private Button _quitButton;

    private void OnEnable() {
        InitButtons();
    }

    private void InitButtons() {
        _playButton = _root.Q("PlayButton") as Button;
        _quitButton = _root.Q("ExitButton") as Button;

        _playButton?.RegisterCallback<ClickEvent>(OnPlayButtonClicked);
        _quitButton?.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
    }

    private void OnPlayButtonClicked(ClickEvent evt) {
        SceneManager.LoadScene("Game");
    }

    private void OnQuitButtonClicked(ClickEvent evt) {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}