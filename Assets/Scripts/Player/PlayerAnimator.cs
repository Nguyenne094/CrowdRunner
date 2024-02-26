using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform crowdParent;

    public void Run(){
        for(int i = 0;i < crowdParent.childCount; i++){
            Animator animator = crowdParent.GetChild(i).GetComponent<Runner>().GetAnimator();
            animator.SetBool("Running", true);
        }
    }

    public void Idle(){
        for(int i = 0;i < crowdParent.childCount; i++){
            Animator animator = crowdParent.GetChild(i).GetComponent<Runner>().GetAnimator();
            animator.SetBool("Running", false);
        }
    }
}
