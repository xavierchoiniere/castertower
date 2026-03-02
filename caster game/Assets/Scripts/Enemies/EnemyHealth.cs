using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth;
    private EnemyAnimation enemyAnimation;
    void Start()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) //called from spells when they hit the enemy
    {
        currentHealth -= damage;
        enemyAnimation.currentState = EnemyAnimation.EnemyState.Damage;
        if (currentHealth <= 0)
        {
            enemyAnimation.currentState = EnemyAnimation.EnemyState.Dead;
        }
    }

    public void DestroyEnemy() //called from animation event at the end of death animation
    {
        Destroy(gameObject);
    }
}
