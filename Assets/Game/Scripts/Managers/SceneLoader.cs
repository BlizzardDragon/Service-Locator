using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoader : MonoBehaviour
{
    public void Restart() => Addressables.LoadSceneAsync(SaveLoader.GetCurrentScene());
}
