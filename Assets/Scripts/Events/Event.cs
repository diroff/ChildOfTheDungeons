using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Event : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Sprite _eventIcon;
    [SerializeField] protected Player Player;
    [SerializeField] protected RoomController RoomController;
    [SerializeField] protected Room EventRoom;
    [Header("Tutorial")]
    [SerializeField] protected TutorialManager TutorialManager;
    [SerializeField] protected Tutorial Tutorial;
    [SerializeField] protected DirectionTutorial AdditionalTutorial;

    [SerializeField] private int _minimalLevel = 1;
    [SerializeField] private float _panelEnableCouldown = 1.0f;

    protected Spawner Spawner;

    public GameObject Panel => _panel;
    public Room Room => EventRoom;
    public Sprite EventIcon => _eventIcon;
    public int MinimalLevel => _minimalLevel;
    public DirectionTutorial DirectionTutorial => AdditionalTutorial;

    public event UnityAction<bool> Ended;

    public void SetPanelState(bool enabled)
    {
        _panel.SetActive(enabled);
    }

    public virtual void SetEnableEvent(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    private void OnEnable()
    {
        SetSpanwer();
    }

    private IEnumerator EnableEvent()
    {
        SetPanelState(false);
        yield return new WaitForSeconds(_panelEnableCouldown);
        SetPanelState(true);
        ShowMessage(true);
    }

    public virtual void StartEvent()
    {
        SetEnableEvent(true);
        StartCoroutine(EnableEvent());
    }

    public virtual void EndEvent()
    {
        Ended?.Invoke(true);
        SetEnableEvent(false);
        SetPanelState(false);
    }

    private void SetSpanwer()
    {
        Spawner = RoomController.GetRoomSpawner();
    }

    protected void ShowMessage(bool enabled)
    {
        if (TutorialManager == null)
            return;

        if (enabled)
            TutorialManager.AddMessages(Tutorial);
        else
            TutorialManager.HideMessage();
    }
}