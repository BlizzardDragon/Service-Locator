using System.Collections;
using UnityEngine;

public class EnemyDestroyController : MonoBehaviour, IGameStartListener, IGameFinishListener
{
    private DestroyService _destroyService;
    private const int DELAY = 2;


    public void OnStartGame()
    {
        _destroyService = ServiceLocator.GetService<DestroyService>();
        ServiceLocator.GetService<PlayerDeathTrigger>().OnEnemyEnter += DestroyGameObject;

    }

    public void OnFinishGame()
    {
        ServiceLocator.GetService<PlayerDeathTrigger>().OnEnemyEnter -= DestroyGameObject;
    }

    private void DestroyGameObject(Collider collider)
    {
        if (collider.attachedRigidbody)
        {
            StartCoroutine(InvokeDestroy(collider.attachedRigidbody.gameObject));
        }
    }

    private IEnumerator InvokeDestroy(GameObject go)
    {
        yield return new WaitForSeconds(DELAY);
        _destroyService.DestroyGameObject(go);
    }
}