using UnityEngine;


[RequireComponent(typeof(Animator))]

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Avatar idleAvatar, runAvatar, cutAvatar;

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate() {
        Animate();
       /* if (animator.GetBool("isCutting")){
            animator.avatar = cutAvatar;
        }
        */
    }
    private void Animate(){
        if (CustomInputSystem.GetInput() != Vector3.zero){
            animator.SetBool("isRunning", true);
        
            //animator.avatar = runAvatar;
          
        }
        else {
            animator.SetBool("isRunning", false);
            if(!animator.GetBool("isCutting")){
                //animator.avatar = idleAvatar;
            }
        }
    }
}
