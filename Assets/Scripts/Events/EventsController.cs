using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private List<Event> _eventTypes;

    [SerializeField] private Event _continue;
    [SerializeField] private Event _gameOver;
    [SerializeField] private Event _startEvent;

    [SerializeField] private ProgressionController _progression;

    private Event _currentEvent;
    private Event _nextEvent;

    private bool _nextEventSetted = false;

    public Event CurrentEvent => _currentEvent;

    private void Start()
    {
        _currentEvent = _startEvent;
        StartEvent();
    }

    private int ChooseRandomEvent()
    {
        return Random.Range(0, _eventTypes.Count);
    }

    public void SetEvent(int eventNumber, bool isRandom = false)
    {
        int number = 0;

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
            while (number == _progression.LastEvent)
            {
                number = ChooseRandomEvent();
            }
        }

        _currentEvent = _eventTypes[number];
        _progression.SetLastEvent(number);
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
            else
                _currentEvent = _continue;

            StartEvent();
        }  
    }
}