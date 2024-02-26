using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private bool isTarget;
    [SerializeField] private Animator animator;

    public bool IsTarget(){
        return isTarget;
    }

    public void SetTarget()
    {
        isTarget = true;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
    }
}
