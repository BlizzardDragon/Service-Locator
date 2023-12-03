using UnityEngine;

public class EnemyScoreObserver : MonoBehaviour, IGameStartListener, IGameFinishListener
{
    private UiManager _uiManager;


    public void OnStartGame()
    {
        _uiManager = ServiceLocator.GetService<UiManager>();
        ServiceLocator.GetService<PlayerDeathTrigger>().OnEnemyEnter += AddScore;
    }

    public void OnFinishGame()
    {
        ServiceLocator.GetService<PlayerDeathTrigger>().OnEnemyEnter -= AddScore;
    }

    private void AddScore(Collider _) => _uiManager.AddScore();
}