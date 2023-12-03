using UnityEngine;

public class DestroyService : MonoBehaviour, IService
{
    public void DestroyGameObject(GameObject go) => Destroy(go);
}