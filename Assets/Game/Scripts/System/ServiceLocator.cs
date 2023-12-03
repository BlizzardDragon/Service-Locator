using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly List<IService> _services = new List<IService>();


    public static T GetService<T>()
    {
        foreach (var service in _services)
        {
            if (service is T result)
            {
                return result;
            }
        }

        throw new System.Exception($"Service of type {typeof(T).Name} is not found!");
    }

    public static List<T> GetServices<T>()
    {
        var result = new List<T>();
        foreach (var service in _services)
        {
            if (service is T tService)
            {
                result.Add(tService);
            }
        }

        return result;
    }

    public static void AddService(IService newService)
    {
        foreach (var service in _services)
        {
            if (service == newService)
            {
                throw new System.Exception($"Service object of type {service.GetType().Name} is already in the list!");
            }
            else
            {
                if (service.GetType() == newService.GetType())
                {
                    Debug.LogWarning($"Service of type {service.GetType().Name} is already in the list!");
                }
            }
        }

        _services.Add(newService);
    }

    public static void ClearServices() => _services.Clear();
}
