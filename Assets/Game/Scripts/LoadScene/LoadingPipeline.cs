using UnityEngine;

public class LoadingPipeline : MonoBehaviour
{
    [SerializeField] private LoadingTask[] _loadingTasks;
    [SerializeField] private LoadingTask _sceneLoadTask;


    private async void Start()
    {
        foreach (var task in _loadingTasks)
        {
            await task.Do();
        }

        await _sceneLoadTask.Do();
    }
}
