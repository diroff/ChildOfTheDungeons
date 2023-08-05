using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Event> _levelEvents;
    [SerializeField] private EventsController _eventsController;

    private Queue<Event> _eventsQueue;

    private void Awake()
    {
        _eventsQueue = new Queue<Event>();

        foreach (Event levelEvent in _levelEvents)
        {
            _eventsQueue.Enqueue(levelEvent);
        }
    }

    private void Start()
    {
        SetNextEvent();
    }

    public void SetNextEvent()
    {
        var nextEvent = _eventsQueue.Dequeue();

        _eventsController.SetEvent(nextEvent); 
    }
}