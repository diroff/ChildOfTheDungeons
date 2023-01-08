using TMPro;
using UnityEngine;

public class LevelDisplayItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void OnEnable()
    {
        _item.LevelChanged += DisplayLevel;
    }

    private void OnDisable()
    {
        _item.LevelChanged -= DisplayLevel;
    }

    public void DisplayLevel(int level)
    {
        _levelText.text = $"{level}";
    }
}