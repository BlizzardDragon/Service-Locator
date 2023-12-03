using UnityEngine;
using System;

public class JumpInput : MonoBehaviour, IGameUpdateListener, IService
{
    public event Action<int> OnMovedToSide;


    public void OnUpdate(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.A))
        {
            OnMovedToSide?.Invoke(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || 
                 Input.GetKeyDown(KeyCode.D) || 
                 Input.GetKey(KeyCode.RightArrow) || 
                 Input.GetKey(KeyCode.D))
        {
            OnMovedToSide?.Invoke(1);
        }
    }
}
