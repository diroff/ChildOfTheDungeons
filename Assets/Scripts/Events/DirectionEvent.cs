using System.Collections.Generic;
using UnityEngine;

public class DirectionEvent : Event
{
    [SerializeField] private List<Event> _availableEvents;
    [SerializeField] private Sign _signTemplate;

    [Header("Controllers")]
    [SerializeField] private EventsController _eventController;
    [SerializeField] private ProgressionController _progressionController;

    private List<Event> _events = new List<Event>();
    private int _directionCount;
    private Sign _sign;

    private List<Event> _currentEvents = new List<Event>();

    public override void StartEvent()
    {
        _currentEvents.Clear();
        UpdateEventList();
        base.StartEvent();
        Spawner.SpawnSign(_signTemplate);
        _sign = Spawner.GetSign();

        DisableDirection(_sign.MaximumDirectionCount);

        SetDirectionCount();
        AddRandomEvents(_directionCount);
    }

    public void AddRandomEvents(int directionCount)
    {
        for (int i = 0; i < directionCount; i++)
        {
            Event randomEvent;
            randomEvent = _events[Random.Range(0, _events.Count)];

            _sign.Directions[i].gameObject.SetActive(true);
            _sign.Directions[i].SetIcon(randomEvent.EventIcon);

            _currentEvents.Add(randomEvent);

            if(_events.Count > 1)
                _events.Remove(randomEvent);
        }
    }

    private void SetDirectionCount()
    {
        _directionCount = Random.Range(_sign.MinimumDirectionCount, _sign.MaximumDirectionCount + 1);
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
        TutorialManager.AddMessages(_currentEvents[directionNumber].DirectionTutorial);

        EndEvent();
    }

    private void UpdateEventList()
    {
        _events.Clear();
        int playerLevel = _progressionController.Player.GetLevel();

        for (int i = 0; i < _availableEvents.Count; i++)
        {
            if (playerLevel >= _availableEvents[i].MinimalLevel && playerLevel <= _availableEvents[i].MaximumLevel)
                _events.Add(_availableEvents[i]);
        }
    }

    public void ClearOutliners()
    {
        for (int i = 0; i < _sign.Directions.Count; i++)
        {
            _sign.Directions[i].SetOutlineState(false);
        }
    }

    public void DisableDirectionInteraction()
    {
        for (int i = 0; i < _sign.Directions.Count; i++)
        {
            _sign.Directions[i].DisableDirection();
        }
    }

    public override void EndEvent()
    {
        _eventController.SetDirection(true);
        base.EndEvent();
    }
}