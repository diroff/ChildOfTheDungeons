using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] protected GameObject GamePanel;

    protected float Result;

    protected virtual void OnEnable()
    {
        SetPanelState(true);
    }

    public void SetPanelState(bool active)
    {
        GamePanel.SetActive(active);
    }

    public virtual float GetGameResult()
    {
        return Result;
    }
}
