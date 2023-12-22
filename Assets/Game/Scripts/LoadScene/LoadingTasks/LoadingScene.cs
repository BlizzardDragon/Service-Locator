using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "LoadingScene", menuName = "Configs/Tasks/LoadingScene", order = 0)]
public class LoadingScene : LoadingTask
{
    public override Task<Result> Do()
    {
        Addressables.LoadSceneAsync(SaveLoader.GetCurrentScene());

        return Task.FromResult(new Result { Success = true });
    }
}
