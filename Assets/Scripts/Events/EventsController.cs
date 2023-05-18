using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private List<Event> _eventTypes;
    [Header("Events")]
    [SerializeField] private Event _continue;
    [SerializeField] private Event _gameOver;
    [SerializeField] private Event _startEvent;
    [Space]
    [SerializeField] private ProgressionController _progression;

    private Event _currentEvent;
    private Event _nextEvent;

    private bool _nextEventSetted = false;
    private bool _needContinue = true;
    private bool _isDirection = false; 

    public Event CurrentEvent => _currentEvent;
    public bool IsDirection => _isDirection;

    private void Start()
    {
        _currentEvent = _startEvent;
        StartEvent();
    }

    private int ChooseEventNumber()
    {
        return Random.Range(0, _eventTypes.Count);
    }

    public void SetEvent(int eventNumber, bool isRandom = false)
    {
        int number = 0;
        int playerLevel = _progression.Player.GetLevel();

        if (_nextEventSetted)
        {
            _currentEvent = _nextEvent;
            _nextEventSetted = false;
            return;
        }

        if (!isRandom) 
            number = eventNumber;
        else
        {
            bool canBeUsed = false;

            while (!canBeUsed)
            {
                number = ChooseEventNumber();

                if(number != _progression.LastEvent)
                    canBeUsed = true;

                if (!IsCorrectLevel(_progression.Player.GetLevel(), number))
                    canBeUsed = false;

                if (GetCountAvailableEvents() <= 1)
                    canBeUsed = true;
            }
        }

        _currentEvent = _eventTypes[number];
        _progression.SetLastEvent(number);
    }

    private int GetCountAvailableEvents()
    {
        int eventsCount = 0;

        for (int i = 0; i < _eventTypes.Count; i++)
        {
            if(IsCorrectLevel(_progression.Player.GetLevel(), i))
                eventsCount++;
        }

        return eventsCount;
    }

    private bool IsCorrectLevel(int playerLevel, int numberEvent)
    {
        return playerLevel >= _eventTypes[numberEvent].MinimalLevel  && playerLevel < _eventTypes[numberEvent].MaximumLevel;
    }

    public void SetEvent(Event newEvent)
    {
        _currentEvent = newEvent;
    }

    public void SetContinue(bool enabled)
    {
        _needContinue = enabled;
    }

    public void SetNextEvent(Event nextEvent)
    {
        _nextEvent = nextEvent;
        _nextEventSetted = true;
    }

    public void StartEvent()
    {
        _currentEvent.StartEvent();
        _currentEvent.Ended += EndCurrentEvent;
    }

    public void EndCurrentEvent(bool ended)
    {
        if (ended)
        {
            _currentEvent.Ended -= EndCurrentEvent;

            if (_player.Die())
                _currentEvent = _gameOver;
            else if (_needContinue)
                _currentEvent = _continue;
            else
                return;

            _needContinue = true;
            StartEvent();
        }  
    }

    public void SetDirection(bool isDirection)
    {
        _isDirection = isDirection;
    }
}