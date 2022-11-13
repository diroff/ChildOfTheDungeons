using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonAction : MonoBehaviour
{
    [SerializeField] private Button _button;

    public abstract void OnClickAction();
}
