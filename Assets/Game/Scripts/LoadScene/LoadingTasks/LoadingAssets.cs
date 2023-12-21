using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadingAssets", menuName = "Configs/Tasks/LoadingAssets", order = 0)]
public class LoadingAssets : LoadingTask
{
    public override async Task<Result> Do()
    {
        var catalog = Resources.Load<PrefabCatalog>(nameof(PrefabCatalog));
        ServiceLocator.AddService(catalog, true);
        await catalog.LoadAssets();

        return new Result { Success = true };
    }
}