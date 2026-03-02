using UnityEngine;


public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    public enum EnemyState { Move, Damage, Dead }
    public EnemyState currentState;
    private EnemyState pastState;

    void Start()
    {
        animator = GetComponent<Animator>();
        pastState = currentState;
    }

    void Update()
    {
        if (pastState != currentState)
        {
            foreach (AnimatorControllerParameter param in animator.parameters)
            {
                if (param.type == AnimatorControllerParameterType.Bool)
                    animator.SetBool(param.name, param.name == currentState.ToString());
            }
            pastState = currentState;
        }
    }
}
