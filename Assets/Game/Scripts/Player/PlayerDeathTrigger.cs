using UnityEngine;
using System;

public class PlayerDeathTrigger : MonoBehaviour, IService
{
    public event Action<Collider> OnEnemyEnter;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyCollider>())
        {
            OnEnemyEnter?.Invoke(other);
        }
    }
}
