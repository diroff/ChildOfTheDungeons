using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _info;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private TextMeshProUGUI _level;

    public void ShowInfo(bool enabled)
    {
        _panel.SetActive(enabled);
    }

    public void SetInfo(string info, float value, float level, bool isPercent)
    {
        _info.text = info;
        _value.text = value.ToString();
        if (isPercent)
            _value.text +="%";
        _level.text = level.ToString();
    }

    public void SetInfo(string info)
    {
        _info.text = info;
    }
}
