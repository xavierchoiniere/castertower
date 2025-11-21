using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float cooldown;
    public int facingIndex;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(transform.localScale.x * facingIndex, transform.localScale.y, transform.localScale.z);
    }
    void Update()
    {
        rb.linearVelocity = new Vector2(speed * facingIndex, 0f);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }
}
