using System.Collections;
using UnityEngine;

public class MeleeSpell : Spell
{
    protected override void Update()
    {
        base.Update();
        if (lifeTime <= 0)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimation>().currentState = PlayerAnimation.PlayerState.Idle;
    }
}
