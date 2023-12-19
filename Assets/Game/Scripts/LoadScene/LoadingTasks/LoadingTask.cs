using System.Threading.Tasks;
using UnityEngine;

public abstract class LoadingTask : ScriptableObject
{
    public abstract Task<Result> Do();

    public struct Result
    {
        public bool Success;
        public string Error;
    }
}