using TMPro;
using UnityEngine;

public class LevelDisplayItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    [SerializeField] private TextMeshProUGUI _levelText;

    private void OnEnable()
    {
        DisplayLevel();
    }

    public void DisplayLevel()
    {
        _levelText.text = "" + _item.GetLevel();
    }
}
