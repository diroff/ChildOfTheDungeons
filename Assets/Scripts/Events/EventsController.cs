using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private List<Event> _eventTypes;

    [SerializeField] private Event _continue;
    [SerializeField] private Event _gameOver;
    [SerializeField] private Event _startEvent;

    private Event _currentEvent;

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
        int number;

        if (!isRandom) number = eventNumber;
        else number = ChooseRandomEvent();

        _currentEvent = _eventTypes[number];
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