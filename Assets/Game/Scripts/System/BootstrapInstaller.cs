using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ServiceLocatorInstaller))]
public sealed class BootstrapInstaller : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private Transform _startButtonParant;


    private void Awake()
    {
        InstallServices();
        InstallGameManager();
        ServiceLocator.GetService<IRoadTarget>().InstallTarget();
    }

    private void OnEnable()
    {
        var gameManager = ServiceLocator.GetService<GameManager>();

        ServiceLocator.GetService<CountdownObserver>().OnGameStarted += gameManager.StartGame;
        ServiceLocator.GetService<CountdownObserver>().OnGameResumed += gameManager.ResumeGame;
        ServiceLocator.GetService<CollisionDetector>().OnEnemyCollision += gameManager.FinishGame;
    }

    private void OnDisable()
    {
        var gameManager = ServiceLocator.GetService<GameManager>();

        ServiceLocator.GetService<CountdownObserver>().OnGameStarted -= gameManager.StartGame;
        ServiceLocator.GetService<CountdownObserver>().OnGameResumed -= gameManager.ResumeGame;
        ServiceLocator.GetService<CollisionDetector>().OnEnemyCollision -= gameManager.FinishGame;
    }

    private void Start()
    {
        ServiceLocator.GetService<StartButtonObserver>().Init(
            Instantiate(_startButton, _startButtonParant).GetComponent<Button>(),
            ServiceLocator.GetService<GameManager>(),
            ServiceLocator.GetService<AudioManager>()
        );
    }

    private void OnDestroy()
    {
        ServiceLocator.ResetLocalContext();
    }

    private void InstallServices() => GetComponent<ServiceLocatorInstaller>().InstallServices();

    private void InstallGameManager()
    {
        IGameListener[] listeners = GetComponentsInChildren<IGameListener>();
        foreach (var listener in listeners)
        {
            ServiceLocator.GetService<GameManager>().AddListener(listener);
        }
    }
}