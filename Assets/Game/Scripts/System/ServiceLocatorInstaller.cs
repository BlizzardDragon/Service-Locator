using UnityEngine;

public class ServiceLocatorInstaller : MonoBehaviour
{
    public void InstallServices()
    {
        IService[] initListeners = GetComponentsInChildren<IService>();
        
        foreach (var listener in initListeners)
        {
            ServiceLocator.AddService(listener);
        }
    }
}
