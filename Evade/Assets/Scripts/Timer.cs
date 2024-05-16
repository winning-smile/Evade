using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _timerText;

    private float _elapsedTime;
    private int _diffId = 1;
    private bool _isAlive = true;

    private void Start() {
        GameEvents.Killed.AddListener(StopCount);
        StartCoroutine(TimerCorutine());
    }

    private void StopCount() {
        _isAlive = false;
    }
    
    private IEnumerator TimerCorutine() {
        while (_isAlive) {
            _elapsedTime += Time.deltaTime;
            var seconds = Mathf.FloorToInt(_elapsedTime);
            _timerText.text = $"{seconds:0000}";

            if (seconds > 5 && _diffId == 1) {
                GameEvents.RaiseDiff();
            }
            yield return true;
        }
    }
}