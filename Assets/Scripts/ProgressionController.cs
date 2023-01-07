using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    [SerializeField] private Player _player;

    private int _lastEvent;

    public int LastEvent => _lastEvent;

    public int SetLevel()
    {
        int playerLevel = _player.GetLevel();

        if (playerLevel > 1)
            return Random.Range(playerLevel - 1, playerLevel + 2);
        else
            return playerLevel;
    }

    public void SetLastEvent(int previousEventNumber)
    {
        _lastEvent = previousEventNumber;
    }
}
