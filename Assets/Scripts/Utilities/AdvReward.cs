using System.Runtime.InteropServices;
using UnityEngine;

public class AdvReward : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void WatchAdv();

    [SerializeField] private Fight _fightEvent;

    public void ShowRewardAdv()
    {
        _fightEvent.WatchReward();
    }

    public void RewardButtonClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        WatchAdv();
#else
        _fightEvent.WatchReward();
#endif
    }
}