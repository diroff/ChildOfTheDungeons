using UnityEngine;

public class ProgressionController : MonoBehaviour
{
    [SerializeField] private Player _player;

    public int SetLevel()
    {
        int playerLevel = _player.GetLevel();

        if (playerLevel > 1)
            return Random.Range(playerLevel - 1, playerLevel + 2);
        else
            return Random.Range(playerLevel, playerLevel + 2);
    }
}
