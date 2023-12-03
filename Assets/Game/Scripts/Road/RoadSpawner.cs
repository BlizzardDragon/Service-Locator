using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class RoadSpawner : MonoBehaviour, IGamePrepareListener, IGameUpdateListener, IService
{
    [SerializeField] private Road _roadPrefab;

    private IRoadTarget _roadTarget;
    private Vector3 _spawnPosition;
    private List<Road> _roads = new();

    private float _oldPositionZ;
    private bool _isFirstStart = true;

    public event Action<Vector3> OnSpawnRoad;


    public void SetRoadTarget(IRoadTarget roadTarget) => _roadTarget = roadTarget;

    public void OnUpdate(float deltaTime)
    {
        float distance = _roadTarget.Transform.position.z - _oldPositionZ;
        if (distance >= _roadPrefab.RoadLength)
        {
            SpawnNextRoad(_spawnPosition, true);
            _oldPositionZ += _roadPrefab.RoadLength;
        }
    }

    public void OnPrepareGame()
    {
        if (!_isFirstStart) return;

        SetTargetStartPosZ();
        SpawnFirstRoad();

        Sequence delay = DOTween.Sequence()
            .SetDelay(0.5f);

        Sequence spawn = DOTween.Sequence()
            .AppendCallback(SpawnStartRoad)
            .AppendInterval(0.5f)
            .SetLoops(5);

        DOTween.Sequence()
            .SetLink(gameObject)
            .Append(delay)
            .Append(spawn);

        _isFirstStart = false;
    }

    private void SetTargetStartPosZ() => _oldPositionZ = _roadTarget.Transform.position.z;

    private void SpawnFirstRoad()
    {
        Vector3 startPosition = _roadTarget.Transform.position - Vector3.forward * _roadPrefab.RoadLength * 2;
        SpawnNextRoad(startPosition, false);
    }

    private void SpawnStartRoad() => SpawnNextRoad(_spawnPosition, false);

    private void SpawnNextRoad(Vector3 currentPosition, bool removeLastRoad)
    {
        Vector3 roadSpawnPosition = GetNextSpawnPosition(currentPosition);
        Road newRoad = Instantiate(_roadPrefab, roadSpawnPosition, Quaternion.identity, transform);
        _roads.Add(newRoad);

        if (removeLastRoad)
        {
            Destroy(_roads[0].gameObject);
            _roads.RemoveAt(0);
            
            OnSpawnRoad?.Invoke(roadSpawnPosition);
        }
    }

    private Vector3 GetNextSpawnPosition(Vector3 currentPosition)
    {
        _spawnPosition = currentPosition;
        return _spawnPosition += Vector3.forward * _roadPrefab.RoadLength;
    }
}
