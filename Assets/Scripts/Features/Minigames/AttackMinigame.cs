using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttackMinigame : Minigame
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _attackPanel;
    [Space]

    [SerializeField] private float _speed;

    [Header("Multiply values")]
    [SerializeField] private float _clearZoneMultiply;
    [SerializeField] private float _redZoneMultiply;
    [SerializeField] private float _orangeZoneMultiply;
    [SerializeField] private float _yellowZoneMultiply;
    [SerializeField] private float _purpleZoneMultiply;
    [Header("Multiply values")]
    [SerializeField] private float _clearZone;
    [SerializeField] private float _redZone;
    [SerializeField] private float _orangeZone;
    [SerializeField] private float _yellowZone;
    [SerializeField] private float _purpleZone;

    private bool _isMoving = false;

    public UnityEvent Ended;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartMovingSlider();
    }

    public void StartMovingSlider()
    {
        _slider.value = 0;
        _isMoving = true;
        _attackPanel.SetActive(true);
        StartCoroutine(MoveSlider(0));
    }

    private IEnumerator MoveSlider(float speedModificator)
    {
        while (_isMoving)
        {
            if (_slider.value >= 1)
            {
                StopSlider();
                break;
            }

            _slider.value += _speed * Time.deltaTime + speedModificator;
            yield return new WaitForSeconds(_speed * Time.deltaTime + speedModificator);
        }
    }

    public override float GetGameResult()
    {
        Result = CalculateDamageModificator();
        return base.GetGameResult();
    }

    public void StopSlider()
    {
        _attackPanel.SetActive(false);
        _isMoving = false;
        SetPanelState(false);
        Ended?.Invoke();
    }

    private float CalculateDamageModificator()
    {
        float sliderValue = _slider.value;

        if ((sliderValue >= _clearZone && sliderValue < _redZone) || (sliderValue <= 1 -_clearZone && sliderValue > 1 - _redZone))
            return _clearZoneMultiply;

        if ((sliderValue >= _redZone && sliderValue < _orangeZone) || (sliderValue <= 1 - _redZone && sliderValue > 1 - _orangeZone))
            return _redZoneMultiply;

        if ((sliderValue >= _orangeZone && sliderValue < _yellowZone) || (sliderValue <= 1 - _orangeZone && sliderValue > 1 - _yellowZone))
            return _orangeZoneMultiply;

        if ((sliderValue >= _yellowZone && sliderValue < _purpleZone) || (sliderValue <= 1 - _yellowZone && sliderValue > 1 - _purpleZone))
            return _yellowZoneMultiply;

        if (sliderValue >= _purpleZone && sliderValue <= 1 - _purpleZone)
            return _purpleZoneMultiply;

        return 0f;
    }
}