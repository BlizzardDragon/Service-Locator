using System;
using UnityEngine;

public class PrefabFactory : MonoBehaviour, IService
{
    [SerializeField] private ParentsInfo[] _parentsInfo;

    private PrefabCatalog _catalog;


    public void Init(PrefabCatalog catalog) => _catalog = catalog;

    public GameObject CreatePrefab(PrefabName key)
    {
        Transform targetParent = null;

        foreach (var parentInfo in _parentsInfo)
        {
            if (parentInfo.Name == key)
            {
                targetParent = parentInfo.Parent;
                break;
            }
        }

        if (targetParent == null)
        {
            throw new Exception("Parent is not found");
        }

        return Instantiate(_catalog.GetPrefab(key), targetParent);
    }

    [Serializable]
    private struct ParentsInfo
    {
        public PrefabName Name;
        public Transform Parent;
    }
}