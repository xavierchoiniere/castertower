using System.Collections;
using UnityEngine;

public class ChargeSpell : Spell
{
 
    public float chargeSpeed;
    public float chargeTime;
    private float currentSpeed;
    private Coroutine chargeRoutine;

    protected override void Start()
    {
        base.Start();
        if (chargeRoutine != null)
            StopCoroutine(chargeRoutine);
        chargeRoutine = StartCoroutine(CastAfterDelay());
        currentSpeed = chargeSpeed;
    }
    protected override void FixedUpdate()
    {
        rb.linearVelocity = transform.right * currentSpeed * facingIndex;
    }

    private IEnumerator CastAfterDelay()
    {
        yield return new WaitForSeconds(chargeTime);
        currentSpeed = speed;
    }
}
