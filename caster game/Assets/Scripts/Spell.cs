using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed;
    public float chargeSpeed;
    public float chargeTime;
    public float lifeTime;
    public float cooldown;
    public int facingIndex;
    private float currentSpeed;
    private Rigidbody2D rb;
    private Coroutine chargeRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(transform.localScale.x * facingIndex, transform.localScale.y, transform.localScale.z);
        if (chargeRoutine != null)
            StopCoroutine(chargeRoutine);
        chargeRoutine = StartCoroutine(CastAfterDelay());
        currentSpeed = chargeSpeed;
    }
    void Update()
    {
        rb.linearVelocity = new Vector2(currentSpeed * facingIndex, 0f);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }
    private IEnumerator CastAfterDelay()
    {
        yield return new WaitForSeconds(chargeTime);
        currentSpeed = speed;
    }
}
