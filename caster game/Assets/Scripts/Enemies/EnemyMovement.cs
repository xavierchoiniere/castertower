using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 scale;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        scale = transform.localScale;
    }
    void Update()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > 0.2f)
        {
            if (player.position.x < transform.position.x) transform.localScale = new Vector3(scale.x * -1, scale.y);
            if (player.position.x > transform.position.x) transform.localScale = scale;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
    }
}
