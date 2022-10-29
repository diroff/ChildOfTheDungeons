using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsController : MonoBehaviour
{
    [SerializeField] private List<Event> _eventTypes;

    private int _currentEventNumber;
    private Event _currentEvent;

    private void Awake()
    {
        SetEvent(0);
    }

    private int ChooseRandomEvent()
    {
        return Random.Range(0, _eventTypes.Count - 1);
    }

    public void InstantiateEvent()
    {
        _currentEvent = Instantiate(_eventTypes[_currentEventNumber]);
    }

    public void DestroyEvent()
    {
        Destroy(_currentEvent.gameObject);
    }

    public void SetEvent(int eventNumber, bool isRandom = false)
    {
        InstantiateEvent();

        int number;

        if (!isRandom) number = eventNumber;
        else number = ChooseRandomEvent();

        _eventTypes[number].DoEventSteps();
        _eventTypes[number].Ended += EndEvent;

        _currentEventNumber = eventNumber;
    }

    public void EndEvent(bool ended)
    {
        if (ended)
        {
            _eventTypes[_currentEventNumber].Ended -= EndEvent;

            Destroy(_currentEvent);

            SetEvent(_eventTypes.Count - 1);
        }
    }
}