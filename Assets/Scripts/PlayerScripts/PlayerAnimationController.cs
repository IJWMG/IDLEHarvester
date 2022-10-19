using UnityEngine;


[RequireComponent(typeof(Animator))]

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _inventoryAnimator;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
            _animator.SetBool("isRunning", true);
            _inventoryAnimator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
            _inventoryAnimator.SetBool("isRunning", false);

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
            _animator.SetBool("isCutting", true);
        }
        else {_animator.SetBool("isCutting", false);}
    }
    
}
