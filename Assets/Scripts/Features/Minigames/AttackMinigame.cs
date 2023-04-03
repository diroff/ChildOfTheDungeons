using UnityEngine;
using UnityEngine.UI;

public class AttackMinigame : Minigame
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _speed;

    private bool _isMoving = false;

    private void Update()
    {
        MoveSlider(0);
    }

    public void StartMovingSlider()
    {
        _isMoving = true;
        _slider.value = 0;
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
        _isMoving = false;
        Debug.Log(GetGameResult());
        SetPanelState(false);
    }
}
