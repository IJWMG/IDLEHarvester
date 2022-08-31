using UnityEngine;


[RequireComponent(typeof(Animator))]

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        GetDistanceToWheet();
        Animate();

    }
    private void Animate()
    {
        if (CustomInputSystem.GetInput() != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    private void GetDistanceToWheet (){
        Physics.SphereCast(new Vector3 (
            transform.position.x, 
            transform.position.y + 0.6f, 
            transform.position.z), 
            1f, transform.forward, out RaycastHit hit, 0.3f);

        Debug.DrawRay(transform.position, transform.forward, Color.red, 1f);
        if ((hit.collider != null) && (hit.collider.tag == "WheetBlock")){
            animator.SetBool("isCutting", true);
        }
        else {animator.SetBool("isCutting", false);}
    }
    
}
