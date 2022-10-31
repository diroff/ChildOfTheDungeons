using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    [SerializeField] private List<Event> _eventTypes;

    private Event _currentEvent;

    public Event CurrentEvent => _currentEvent;

    private void Start()
    {
        SetEvent(0);
        StartEvent();
    }

    private int ChooseRandomEvent()
    {
        return Random.Range(0, _eventTypes.Count - 1);
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
        _currentEvent.DoEventSteps();
        _currentEvent.Ended += EndCurrentEvent;
    }

    public void EndCurrentEvent(bool ended)
    {
        if (ended)
        {
            _currentEvent.Ended -= EndCurrentEvent;

            SetEvent(_eventTypes.Count - 1);
            StartEvent();
        }
    }
}