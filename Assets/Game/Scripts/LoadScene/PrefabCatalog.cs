using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class PrefabCatalog : MonoBehaviour, IService
{
    [SerializeField]
    private PrefabInfo[] _prefabs = Array.Empty<PrefabInfo>();

    public async Task LoadAssets()
    {
        var count = _prefabs.Length;
        var tasks = new Task<GameObject>[count];

        for (var i = 0; i < count; i++)
        {
            var info = _prefabs[i];
            var handle = info.Addressable.LoadAssetAsync<GameObject>();
            tasks[i] = handle.Task;
        }

        var prefabs = await Task.WhenAll(tasks);
        for (var i = 0; i < count; i++)
        {
            var info = _prefabs[i];
            info.Prefab = prefabs[i];
        }
    }

    public GameObject GetPrefab(PrefabName name)
    {
        for (int i = 0, count = _prefabs.Length; i < count; i++)
        {
            var info = _prefabs[i];
            if (info.Name == name)
            {
                return info.Prefab;
            }
        }

        throw new Exception($"Prefab {name} is not found!");
    }

    [Serializable]
    private sealed class PrefabInfo
    {
        [SerializeField]
        public PrefabName Name;

        [SerializeField]
        public AssetReference Addressable;

        public GameObject Prefab;
    }
}