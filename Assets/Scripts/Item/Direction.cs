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

    private void OnMouseDown()
    {
        Select();
    }

    public void SetOutlineState(bool enabled)
    {
        _outline.SetActive(enabled);
    }

    public void SetIcon(Sprite sprite)
    {
        _doorIcon.sprite = sprite;
    }

    private void Select()
    {
        _directionEvent.ClearOutliners();
        _directionEvent.SetDirection(_directionNumber);
        SetOutlineState(true);
    }
}