using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private Spawner _spawner;

    private Animator _doorAnimator;

    private void Awake()
    {
        if (_door != null)
            _doorAnimator = _door.GetComponent<Animator>();
    }

    public void InteractDoor(bool open)
    {
        if (_doorAnimator == null)
            return;

        if (open)
            _doorAnimator.SetTrigger("Open");
        else
            _doorAnimator.SetTrigger("Close");
    }

    public Spawner GetRoomSpawner()
    {
        return _spawner;
    }
}