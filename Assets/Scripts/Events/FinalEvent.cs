using System.Collections;
using UnityEngine;

public class FinalEvent : Event
{
    [SerializeField] private float _walkAnimationTime;
    [SerializeField] private float _endGameTime;
    [SerializeField] private Animator _blackScreen;

    public void FinishGame()
    {
        StartCoroutine(Exit());
    }

    private IEnumerator Exit()
    {
        _blackScreen.gameObject.SetActive(true);
        SetPanelState(false);
        Player.Move();
        yield return new WaitForSeconds(_walkAnimationTime);
        _blackScreen.SetTrigger("On");
        yield return new WaitForSeconds(_endGameTime);
        Debug.Log("Game over!");
    }
}