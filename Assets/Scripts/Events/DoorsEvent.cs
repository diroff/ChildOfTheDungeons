using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsEvent : Event
{
    [SerializeField] private List<Event> _availableEvents;
    [SerializeField] private List<Door> _doors;
    [SerializeField] private int _maxDoorCount = 3;
    [SerializeField] private EventsController _eventController;

    private int _doorsCount;

    private List<Event> _currentEvents = new List<Event>();

    public override void StartEvent()
    {
        base.StartEvent();
        DoorsCount();
        AddRandomEvents(_doorsCount);
    }

    public void AddRandomEvents(int doorsCount)
    {
        for (int i = 0; i < doorsCount; i++)
        {
            Event randomEvent = _availableEvents[Random.Range(0, _availableEvents.Count)];

            _doors[i].gameObject.SetActive(true);
            _doors[i].SetIcon(randomEvent.EventIcon);

            _currentEvents.Add(randomEvent);
        }
    }

    private void DoorsCount()
    {
        _doorsCount = Random.Range(2, _maxDoorCount + 1);
    }

    private void DisableDoors(int doorsCount)
    {
        for (int i = 0; i < doorsCount; i++)
        {
            _doors[i].gameObject.SetActive(false);
        }
    }

    public void OpenDoor(int doorNumber)
    {
        _eventController.SetNextEvent(_currentEvents[doorNumber]);
        EndEvent();
    }

    public override void EndEvent()
    {
        DisableDoors(_doorsCount);
        _currentEvents.Clear();
        base.EndEvent();
    }
}