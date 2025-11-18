using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpell : MonoBehaviour
{
    public List<GameObject> spellList;
    public Transform spellSpawnPoint;
    private int spellIndex;
    private PlayerAnimation playerAnimation;
    private Coroutine castRoutine;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void OnSpell1() => StartCastAnimation(0);
    public void OnSpell2() => StartCastAnimation(1);
    public void OnSpell3() => StartCastAnimation(2);
    public void OnSpell4() => StartCastAnimation(3);
    public void OnSpell5() => StartCastAnimation(4);

    private void StartCastAnimation(int index)
    {
        if (playerAnimation.currentState != PlayerAnimation.PlayerState.Idle && 
            playerAnimation.currentState != PlayerAnimation.PlayerState.Running) return;
        playerAnimation.currentState = PlayerAnimation.PlayerState.Casting;
        spellIndex = index;
        if (castRoutine != null)
            StopCoroutine(castRoutine);
        castRoutine = StartCoroutine(CastAfterDelay());
    }

    private IEnumerator CastAfterDelay()
    {
        yield return new WaitForSeconds(0.24f);
        HandleCastAnimationEnd();
    }

    private void HandleCastAnimationEnd()
    {
        if (playerAnimation.currentState == PlayerAnimation.PlayerState.Casting) 
            playerAnimation.currentState = PlayerAnimation.PlayerState.Idle;
        GameObject spellClone = Instantiate(spellList[spellIndex], spellSpawnPoint.position, Quaternion.identity);
        spellClone.GetComponent<Spell>().facingIndex = (int)Mathf.Sign(transform.localScale.x);
    }
}
