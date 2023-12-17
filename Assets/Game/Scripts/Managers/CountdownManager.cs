using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class CountdownManager : MonoBehaviour, IGamePrepareListener, IService
{
    [SerializeField] private TextMeshProUGUI _countdownText;
    [SerializeField] private Image _background;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _score;

    private Color _backgroundStartColor;
    private Color _textStartColor;

    private int _countdownValue;

    public event Action OnTimeIsOver;

    private const int TIMER_VALUE = 3;
    private const float FADE_TIME = 1;
    private const float SCALE_TEXT = 1.5f;
    private const int LOOPS = TIMER_VALUE;
    private const int BACKGROUND_FADE_TIME = TIMER_VALUE;


    public void OnPrepareGame() => PlayCountdown();

    private void PlayCountdown()
    {
        _backgroundStartColor = _background.color;
        _textStartColor = _countdownText.color;
        _background.enabled = true;
        _countdownText.enabled = true;
        _countdownValue = TIMER_VALUE;

        DOTween.Sequence()
            .AppendCallback(SetText)
            .Append(_countdownText.transform.DOScale(0, 0))
            .Append(_countdownText.transform.DOScale(SCALE_TEXT, FADE_TIME))
            .SetEase(Ease.OutCirc)
            .SetLoops(LOOPS);

        DOTween.Sequence()
            .Append(_countdownText.DOFade(0, FADE_TIME))
            .SetEase(Ease.InQuint)
            .SetLoops(LOOPS);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(_background.DOFade(0, BACKGROUND_FADE_TIME))
            .AppendCallback(ShowGameScreen);
    }

    private void SetText()
    {
        _countdownText.text = _countdownValue.ToString();
        _countdownValue--;
    }

    private void ShowGameScreen()
    {
        _background.enabled = false;
        _background.color = _backgroundStartColor;
        _countdownText.color = _textStartColor;
        _countdownText.enabled = false;
        _pauseButton.SetActive(true);
        _score.SetActive(true);
        OnTimeIsOver?.Invoke();
    }
}
