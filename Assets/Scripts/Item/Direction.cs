using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _doorIcon;
    [SerializeField] private GameObject _outline;
    [SerializeField] private int _directionNumber;

    private DirectionEvent _directionEvent;

    private void OnEnable()
    {
        _directionEvent = FindObjectOfType<DirectionEvent>();
    }

    public void SetIcon(Sprite sprite)
    {
        _doorIcon.sprite = sprite;
    }

    private void OnMouseDown()
    {
        Debug.Log($"Путь: {_directionNumber}");
        Select();
    }

    private void Select()
    {
        _directionEvent.ClearOutliners();
        _directionEvent.SetDirection(_directionNumber);
        SetOutlineState(true);
    }

    public void SetOutlineState(bool enabled)
    {
        _outline.SetActive(enabled);
    }
}