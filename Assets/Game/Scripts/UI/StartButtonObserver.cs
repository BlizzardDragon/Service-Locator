using UnityEngine;
using UnityEngine.UI;

public class StartButtonObserver : MonoBehaviour, IService
{
    [SerializeField] private GameObject _countdownText;

    private Button _button;
    private GameManager _gameManager;
    private AudioManager _audioManager;


    public void Init(Button startButton, GameManager gameManager, AudioManager audioManager)
    {
        _button = startButton;
        _gameManager = gameManager;
        _audioManager = audioManager;

        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable() => _button.onClick.RemoveListener(OnClicked);

    private void OnClicked()
    {
        _gameManager.PrepareForGame();
        _audioManager.PlayStart();

        _countdownText.SetActive(true);
        _button.gameObject.SetActive(false);
    }
}
