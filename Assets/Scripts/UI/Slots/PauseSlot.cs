using UnityEngine;

public class PauseSlot : SimpleDescriptionSlot
{
    public void SetPauseState(bool enabled)
    {
        if (enabled)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}