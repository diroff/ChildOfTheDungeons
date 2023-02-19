using TMPro;
using UnityEngine;

public class LevelDisplayItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void OnEnable()
    {
        _item.LevelChanged.AddListener(level => { _levelText.text = $"{level}"; });
    }
}