using System;
using System.Linq;
using UnityEngine;

public enum FindType
{
    GetComponentsInChildren,
    FindObjectsOfType,
}

public class ServiceLocatorInstaller : MonoBehaviour
{
    [SerializeField] private FindType _findType;


    public void InstallServices()
    {
        IService[] initListeners;

        if (_findType == FindType.GetComponentsInChildren)
        {
            initListeners = GetComponentsInChildren<IService>();
        }
        else if (_findType == FindType.FindObjectsOfType)
        {
            initListeners = FindObjectsOfType<MonoBehaviour>()
               .OfType<IService>()
               .ToArray();
        }
        else
        {
            throw new Exception("Invalid search type");
        }

        foreach (var listener in initListeners)
        {
            ServiceLocator.AddService(listener);
        }
    }
}
