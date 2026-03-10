using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float kbForce;
    private float kbTime;
    private Transform player;
    private Vector2 scale;
    private EnemyAnimation enemyAnimation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyAnimation = GetComponent<EnemyAnimation>();
        scale = transform.localScale;
        kbTime = 0.1f;
    }
    void Update()
    {
        EnemyAnimation.EnemyState currentState = enemyAnimation.currentState;
        switch (currentState)
        {
            case EnemyAnimation.EnemyState.Move:
                if (Mathf.Abs(transform.position.x - player.position.x) > 0.2f)
                {
                    if (player.position.x < transform.position.x) transform.localScale = new Vector3(scale.x * -1, scale.y);
                    if (player.position.x > transform.position.x) transform.localScale = scale;
                }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
                break;

            case EnemyAnimation.EnemyState.Damage:
                if (kbTime > 0)
                {
                    kbTime -= Time.deltaTime;
                    float kbDir = Mathf.Sign(transform.position.x - player.position.x);
                    transform.position = new Vector2(transform.position.x + kbDir * kbForce * Time.deltaTime, transform.position.y);
                }
                if (kbTime <= 0)
                {
                    kbTime = 0.1f;
                    enemyAnimation.currentState = EnemyAnimation.EnemyState.Move;
                }
                break;
        }
    }
}
