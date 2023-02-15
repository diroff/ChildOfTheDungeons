using UnityEngine;
using UnityEngine.Events;

public class Skill : MonoBehaviour
{
    [SerializeField] private int _startLevel;
    [SerializeField] private int _maxLevel;

    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public event UnityAction<int> ValueChanged;

    private void Awake()
    {
        _currentLevel = _startLevel;
        ValueChanged?.Invoke(_currentLevel);
    }

    public void AddLevel(int count)
    {
        _currentLevel += count;

        if (_currentLevel > _maxLevel)
            _currentLevel = _maxLevel;

        ValueChanged?.Invoke(_currentLevel);
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
}