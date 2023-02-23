using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DirectionEvent : Event
{
    [SerializeField] private List<Event> _availableEvents;
    [SerializeField] private EventsController _eventController;
    [SerializeField] private Sign _signTemplate;
    
    private int _directionCount;
    private Sign _sign;

    private List<Event> _currentEvents = new List<Event>();

    public override void StartEvent()
    {
        base.StartEvent();
        Spawner.SpawnSign(_signTemplate);
        _sign = Spawner.GetSign();

        DisableDirection(_sign.MaximumDirectionCount);

        DirectionCount();
        AddRandomEvents(_directionCount);
    }

    public void AddRandomEvents(int directionCount)
    {
        Debug.Log("Будет направлений заспавнено:" + directionCount);

        for (int i = 0; i < directionCount; i++)
        {
            Event randomEvent = _availableEvents[Random.Range(0, _availableEvents.Count)];

            _sign.Directions[i].gameObject.SetActive(true);
            _sign.Directions[i].SetIcon(randomEvent.EventIcon);

            _currentEvents.Add(randomEvent);
            Debug.Log($"заспавнил эвент {randomEvent}");
        }
    }

    private void DirectionCount()
    {
        _directionCount = Random.Range(_sign.MinimumDirectionCount, _sign.MaximumDirectionCount + 1);
        Debug.Log("Будет эвентов" + _directionCount);
    }

    private void DisableDirection(int directionCount)
    {
        for (int i = 0; i < directionCount; i++)
        {
            _sign.Directions[i].gameObject.SetActive(false);
        }
    }

    public void SetDirection(int directionNumber)
    {
        _eventController.SetNextEvent(_currentEvents[directionNumber]);
        EndEvent();
    }

    public void ClearOutliners()
    {
        for (int i = 0; i < _sign.Directions.Count; i++)
        {
            _sign.Directions[i].SetOutlineState(false);
        }
    }
}