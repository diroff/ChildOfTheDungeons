using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealSlot : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _healButton;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetButtonState()
    {
        if (_player.PotionChecker())
        {
            _healButton.interactable = true;
            UpdateHealCount();
        }
        else
        {
            DisableCountText();
            _healButton.interactable = false;
        }
    }

    private void UpdateHealCount()
    {
        _text.gameObject.SetActive(true);
        _text.text = "x" + _player.PotionCount;
    }

    private void DisableCountText()
    {
        _text.gameObject.SetActive(false);
    }
}
