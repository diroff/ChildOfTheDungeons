using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Player Player => _player;

    private int _lastEvent;

    public int LastEvent => _lastEvent;

    public int SetLevel()
    {
        int playerLevel = _player.GetLevel();

        if (playerLevel <= 1)
            return playerLevel;

        return Random.Range(playerLevel - 1, playerLevel + 1);
    }

    public void SetLastEvent(int previousEventNumber)
    {
        _lastEvent = previousEventNumber;
    }
}
