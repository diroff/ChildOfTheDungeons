using UnityEngine;
using UnityEngine.UI;

public class Outline : MonoBehaviour
{
    [SerializeField] private Image _outline;

    public void SetOutline(Transform target)
    {
        _outline.gameObject.SetActive(true);
        _outline.transform.SetParent(target);
        _outline.transform.localPosition = Vector3.zero;
    }

    public void DisableOutline()
    {
        _outline.gameObject.SetActive(false);
    }
}   