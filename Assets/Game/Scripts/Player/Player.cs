using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour, 
    IService, 
    IRoadTarget, 
    IGamePrepareListener, 
    IGameResumeListener, 
    IGameFinishListener
{
    public Transform Transform => transform;
    private Rigidbody _rigidbody;


    public void InstallTarget() => ServiceLocator.GetService<RoadSpawner>().SetRoadTarget(this);

    public void OnPrepareGame() => _rigidbody = GetComponent<Rigidbody>();

    public void OnResumeGame()
    {
        // Заставляю Rigidbody проснуться. WakeUp не сработает, так как Rigidbody isKinematic.
        _rigidbody.interpolation = RigidbodyInterpolation.None;
        _rigidbody.MovePosition(_rigidbody.position);
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void OnFinishGame()
    {
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.isKinematic = false;
    }
}
