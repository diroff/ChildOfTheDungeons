using UnityEngine;
using UnityEngine.UI;

public class AttackMinigame : Minigame
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _speed;

    [SerializeField] private GameObject _attackPanel;

    private bool _isMoving = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartMovingSlider();
    }

    private void Update()
    {
        MoveSlider(0);
    }

    public void StartMovingSlider()
    {
        _slider.value = 0;
        _isMoving = true;
        _attackPanel.SetActive(true);
    }

    public void MoveSlider(float speedModificator)
    {
        if (!_isMoving)
            return;

        if (_slider.value >= 1)
        {
            StopSlider();
            return;
        }

        _slider.value += (_speed + speedModificator) * Time.deltaTime;
    }

    public override float GetGameResult()
    {
        Result = _slider.value;
        return base.GetGameResult();
    }

    public void StopSlider()
    {
        _attackPanel.SetActive(false);
        _isMoving = false;
        SetPanelState(false);
    }
}
