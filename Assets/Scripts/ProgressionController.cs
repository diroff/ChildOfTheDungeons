using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    [SerializeField] private Player _player;

    public int CurrentPoints { get; private set; }

    private int _lastEvent;
    private Item _lastItem;

    public Player Player => _player;
    public int LastEvent => _lastEvent;
    public Item LastItem => _lastItem;

    private void OnEnable()
    {
        _player.ExperienceAdded.AddListener(AddPoints);
    }

    private void OnDisable()
    {
        _player.ExperienceAdded.RemoveListener(AddPoints);
    }

    public void AddPoints(int count)
    {
        CurrentPoints += count;
    }

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

    public void SetLastItem(Item item)
    {
        _lastItem = item;
    }
}