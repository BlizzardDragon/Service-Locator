using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpComponent : MonoBehaviour,
    IGameStartListener,
    IGameResumeListener,
    IGamePauseListener,
    IGameFinishListener
{
    [SerializeField] private Transform _view;
    [SerializeField] private Transform[] _jumpTargets;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    private int _currentPosition = 1;
    private bool _isGrounded = true;
    private bool _movementIsAllowed;

    private const float JUMP_DURATION = 0.4f;

    public event Action OnJump;


    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void OnStartGame() => Subscribe();
    public void OnResumeGame() => Subscribe();
    public void OnPauseGame() => Unsubscribe();
    public void OnFinishGame() => Unsubscribe();

    private void Subscribe()
    {
        ServiceLocator.GetService<JumpInput>().OnMovedToSide += PrepareToJump;
        _movementIsAllowed = true;
    }

    private void Unsubscribe()
    {
        ServiceLocator.GetService<JumpInput>().OnMovedToSide -= PrepareToJump;
        _movementIsAllowed = false;
    }

    private void PrepareToJump(int offsetDirection)
    {
        if (_isGrounded)
        {
            int newPosition = _currentPosition + offsetDirection;

            if (newPosition < 0)
            {
                newPosition = 0;
            }

            if (newPosition > _jumpTargets.Length - 1)
            {
                newPosition = _jumpTargets.Length - 1;
            }

            _coroutine = StartCoroutine(Jump(newPosition, offsetDirection));
        }
    }

    private IEnumerator Jump(int newPosition, int offsetDirection)
    {
        OnJump?.Invoke();

        _isGrounded = false;
        int oldPosition = _currentPosition;
        _currentPosition = newPosition;

        for (float t = 0; t < 1; t += Time.deltaTime / JUMP_DURATION)
        {
            Vector3 position = Vector3.Lerp(_jumpTargets[oldPosition].position, _jumpTargets[newPosition].position, t);
            Vector3 sinPosition = new Vector3(position.x, position.y + Mathf.Sin(t * Mathf.PI), position.z);
            Vector3 rotation;

            if (offsetDirection < 0)
            {
                rotation = new Vector3(0, 0, Mathf.Sin(t * Mathf.PI / 2) * 180);
            }
            else
            {
                rotation = new Vector3(0, 0, Mathf.Sin((1 - t) * Mathf.PI / 2) * 180);
            }

            _rigidbody.MovePosition(sinPosition);
            _view.rotation = Quaternion.Euler(rotation);

            if (_movementIsAllowed)
            {
                yield return null;
            }
            else
            {
                yield return new WaitUntil(() => _movementIsAllowed);
            }
        }

        _view.rotation = Quaternion.identity;
        _rigidbody.MovePosition(_jumpTargets[newPosition].position);
        CompleteJump();
    }

    private void CompleteJump() => _isGrounded = true;
}
