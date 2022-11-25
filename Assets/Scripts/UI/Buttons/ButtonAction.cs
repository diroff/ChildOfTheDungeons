using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonAction : MonoBehaviour
{
    [SerializeField] protected Button Button;

    public abstract void OnClickAction();
}
