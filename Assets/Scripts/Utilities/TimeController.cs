using UnityEngine;

public class TimeController : MonoBehaviour
{
    public void OnEnable()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        Time.timeScale = 1;
    }
}