using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreField;

    public void SetScore(string score)
    {
        _scoreField.text = score;
    }

    public void HideRecord()
    {
        gameObject.SetActive(false);
    }
}