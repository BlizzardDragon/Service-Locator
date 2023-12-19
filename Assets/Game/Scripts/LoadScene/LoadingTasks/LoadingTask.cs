using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadingTask", menuName = "Configs/LoadingTask", order = 0)]
public abstract class LoadingTask : ScriptableObject
{
    public abstract Task<Result> Do();

    public struct Result
    {
        public bool Success;
        public string Error;
    }
}