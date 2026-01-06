using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    public enum PlayerState { Idle, Running, Jumping, Falling, Dashing, Casting, HoldCasting }
    public PlayerState currentState;
    private PlayerState pastState;

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
