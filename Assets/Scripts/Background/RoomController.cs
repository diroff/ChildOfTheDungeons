using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundObject;
    [Header("Room templates")]
    [SerializeField] private Room _defaultRoom;
    [SerializeField] private Room _forkRoom;
    [Space]
    [SerializeField] private Transform _startSpawnPoint;
    [SerializeField] private float _zSpacing;
    [SerializeField] private int _maxRoomsCount = 3;
    [SerializeField] private float _speed = 4f;

    public Room DefaultRoom => _defaultRoom;
    public Room ForkRoom => _forkRoom;

    private List<Room> _currentRooms = new List<Room>();

    private float _lastZPoint;
    private float _targetPosition;

    private void Awake()
    {
        Room[] backgroundObjects = _backgroundObject.GetComponentsInChildren<Room>();

        for (int i = 0; i < backgroundObjects.Length; i++)
        {
            _currentRooms.Add(backgroundObjects[i]);
        }
    }

    private void Start()
    {
        _lastZPoint = _zSpacing / 2;
        _targetPosition = transform.position.z;
    }

    public void SpawnRoom(Room roomTemplate)
    {
        _currentRooms[_currentRooms.Count - 1].InteractDoor(true);

        Room room = Instantiate(roomTemplate, SpawnPoint(), Quaternion.identity);
        room.transform.parent = _backgroundObject.transform;
        _currentRooms.Add(room);

        _targetPosition = transform.position.z - 10;
        RemoveRoom();
    }

    private Vector3 SpawnPoint()
    {
        Vector3 spawnPoint = new Vector3(_startSpawnPoint.position.x, _startSpawnPoint.position.y, _lastZPoint + _zSpacing);
        _lastZPoint = spawnPoint.z;

        return spawnPoint;
    }

    [ContextMenu("Move Background")]
    public void MoveBackground()
    {
        StartCoroutine(MoveBackgroundCoroutine());
    }

    public Room GetCurrentRoom()
    {
        return _currentRooms[_currentRooms.Count - 1];
    }

    public Spawner GetRoomSpawner()
    {
        return GetCurrentRoom().GetRoomSpawner();
    }

    private IEnumerator MoveBackgroundCoroutine()
    {
        while (_backgroundObject.transform.position.z > _targetPosition)
        {
            _backgroundObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1 * _speed * Time.deltaTime);
            yield return null;
        }

        _lastZPoint = _zSpacing / 2;
        _targetPosition = transform.position.z - 10;
    }

    [ContextMenu("Destroy Room")]
    private void RemoveRoom()
    {
        if (_currentRooms.Count <= _maxRoomsCount)
            return;

        Destroy(_currentRooms[0].gameObject);
        _currentRooms.RemoveAt(0);
    }
}