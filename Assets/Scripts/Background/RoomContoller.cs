using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContoller : MonoBehaviour
{
    [SerializeField] private GameObject _backgroundObject;
    [Header("Room templates")]
    [SerializeField] private Room _defaultRoom;
    [SerializeField] private Room _forkRoom;
    [Space]
    [SerializeField] private Transform _startSpawnPoint;
    [SerializeField] private float _zSpacing;
    [SerializeField] private int _maxRoomsCount = 3;

    private List<Room> _currentRooms = new List<Room>();

    private float _lastZPoint;
    private float targetPosition;
    private float speed = 0.1f;

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
        _lastZPoint = _startSpawnPoint.position.z + _zSpacing;
        SpawnStartRooms();
    }

    private void SpawnStartRooms()
    {
        _lastZPoint = _zSpacing / 2;
        targetPosition = transform.position.z;
    }

    public void SpawnRoom(Room roomTemplate)
    {
        _currentRooms[_currentRooms.Count - 1].InteractDoor(true);

        Room room = Instantiate(roomTemplate, SpawnPoint(), Quaternion.identity);
        room.transform.parent = _backgroundObject.transform;
        _currentRooms.Add(room);

        targetPosition = transform.position.z - 10;
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
        if (_backgroundObject.transform.position.z > targetPosition)
            _backgroundObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1 * speed);

        _lastZPoint = _zSpacing / 2;
    }

    private void FixedUpdate()
    {
        MoveBackground();
    }

    [ContextMenu("Spawn default room")]
    private void TestSpawning()
    {
        SpawnRoom(_defaultRoom);
    }

    [ContextMenu("Spawn fork room")]
    private void SpawnForkRoom()
    {
        SpawnRoom(_forkRoom);
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