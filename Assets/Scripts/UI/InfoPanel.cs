using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _info;
    [SerializeField] private TextMeshProUGUI _value;

    public void ShowInfo(bool enabled)
    {
        _panel.SetActive(enabled);
    }

    public void SetInfo(string info, float value, bool isPercent)
    {
        _info.text = info;
        _value.text = value.ToString();
        if (isPercent)
            _value.text +="%";
    }

    public void SetInfo(string info)
    {
        if(_info != null)
            _info.text = info;
    }
}