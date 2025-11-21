using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpell : MonoBehaviour
{
    public Transform spellSpawnPoint;
    [SerializeField] private List<GameObject> spellList;
    private List<float> spellCDList;
    private int spellIndex;
    private PlayerAnimation playerAnimation;
    private Coroutine castRoutine;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        spellCDList = new List<float>(new float[5]);
    }

    void Update()
    {
        for (int i = 0; i < spellCDList.Count; i++)
        {
            if (spellCDList[i] > 0) spellCDList[i] -= Time.deltaTime;
            if (spellCDList[i] <= 0) spellCDList[i] = 0;
        }
    }

    public void OnSpell1() => StartCastAnimation(0);
    public void OnSpell2() => StartCastAnimation(1);
    public void OnSpell3() => StartCastAnimation(2);
    public void OnSpell4() => StartCastAnimation(3);
    public void OnSpell5() => StartCastAnimation(4);

    private void StartCastAnimation(int index)
    {
        if (spellCDList[index] > 0) return;
        if (index >= spellList.Count) return;
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
        yield return new WaitForSeconds(0.2f);
        HandleCastAnimationEnd();
    }

    private void HandleCastAnimationEnd()
    {
        spellCDList[spellIndex] = spellList[spellIndex].GetComponent<Spell>().cooldown;
        if (playerAnimation.currentState == PlayerAnimation.PlayerState.Casting) 
            playerAnimation.currentState = PlayerAnimation.PlayerState.Idle;
        GameObject spellClone = Instantiate(spellList[spellIndex], spellSpawnPoint.position, Quaternion.identity);
        spellClone.GetComponent<Spell>().facingIndex = (int)Mathf.Sign(transform.localScale.x);
    }
}
