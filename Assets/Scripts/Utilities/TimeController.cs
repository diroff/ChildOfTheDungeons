using UnityEngine;

public class TimeController : MonoBehaviour
{
    public void OnEnable()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }
}