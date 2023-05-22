using UnityEngine;
using UnityEngine.Events;

public class Skill : MonoBehaviour
{
    [SerializeField] private int _startLevel;
    [SerializeField] private int _maxLevel;
    [TextArea(1, 2)]
    [SerializeField] private string _description;
    [SerializeField] private Tutorial _tutorial;

    private int _currentLevel;

    public int CurrentLevel => _currentLevel;
    public string Description => _description;
    public Tutorial Tutorial => _tutorial;

    public UnityEvent<int> ValueChanged;

    private void Awake()
    {
        _currentLevel = _startLevel;
    }

    private void Start()
    {
        ValueChanged?.Invoke(_currentLevel);
    }

    public bool AddLevel(int count)
    {
        if (_currentLevel >= _maxLevel)
            return false;

        _currentLevel += count;
        ValueChanged?.Invoke(_currentLevel);
        return true;
    }

    public void ChangeLevel(int value)
    {
        _currentLevel = value;

        if (_currentLevel < _startLevel)
            _currentLevel = _startLevel;
        else if (_currentLevel > _maxLevel)
            _currentLevel = _maxLevel;

        ValueChanged?.Invoke(_currentLevel);
    }

    public bool CanBeUpgraded()
    {
        return _currentLevel < _maxLevel;
    }
}