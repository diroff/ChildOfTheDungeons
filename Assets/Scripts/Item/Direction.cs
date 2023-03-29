using UnityEngine;

public class Direction : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _doorIcon;
    [SerializeField] private GameObject _outline;
    [SerializeField] private int _directionNumber;

    private DirectionEvent _directionEvent;
    private BoxCollider2D _boxCollider;

    private void OnEnable()
    {
        _directionEvent = FindObjectOfType<DirectionEvent>();
        _boxCollider = GetComponent<BoxCollider2D>();
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

    public void DisableDirection()
    {
        _boxCollider.enabled = false;
    }
}