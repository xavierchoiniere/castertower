using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float cooldown;
    public int projectileAmount;
    public int facingIndex;
    protected Rigidbody2D rb;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(transform.localScale.x * facingIndex, transform.localScale.y, transform.localScale.z);
    }
    protected virtual void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed * facingIndex;
    }
    protected virtual void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }

}
