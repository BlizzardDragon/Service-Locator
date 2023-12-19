using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LoadingScene", menuName = "Configs/Tasks/LoadingScene", order = 0)]
public class LoadingScene : LoadingTask
{
    public override Task<Result> Do()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        return Task.FromResult(new Result { Success = true });
    }
}