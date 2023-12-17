using System;
using UnityEngine;

public class CountdownObserver : MonoBehaviour, IService
{
    [SerializeField] private CountdownManager _countdownText;
    private bool _isFirstStart = true;

    public event Action OnGameStarted;
    public event Action OnGameResumed;


    private void OnEnable() => _countdownText.OnTimeIsOver += InvokeChangeStateEvent;
    private void OnDisable() => _countdownText.OnTimeIsOver += InvokeChangeStateEvent;

    private void InvokeChangeStateEvent()
    {
        if (_isFirstStart)
        {
            OnGameStarted?.Invoke();
            _isFirstStart = false;
        }
        else
        {
            OnGameResumed?.Invoke();
        }
    }
}