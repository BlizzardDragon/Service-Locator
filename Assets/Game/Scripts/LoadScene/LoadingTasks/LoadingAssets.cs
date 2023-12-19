using System.Threading.Tasks;
using UnityEngine;

public class LoadingAssets : LoadingTask
{
    [SerializeField] private PrefabCatalog _catalog;


    public override async Task<Result> Do()
    {
        await _catalog.LoadAssets();

        return new Result { Success = true };
    }
}