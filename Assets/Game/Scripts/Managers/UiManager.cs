using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour, IService, IGameFinishListener
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _loseScreen;

    private float _score;

    private const float INVOKE_TIME = 3;


    private void Awake() => UpdateScoreText();

    public void OnFinishGame() => Invoke(nameof(ShowLoseScreen), INVOKE_TIME);

    public void UpdateScoreText() => _scoreText.text = _score.ToString();
    private void ShowLoseScreen() => _loseScreen.SetActive(true);

    public void AddScore()
    {
        _score++;
        UpdateScoreText();
    }
}
