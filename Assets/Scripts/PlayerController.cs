using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static float _runSpeed = 5.5f;
    public static float RunSpeed { get { return _runSpeed; } private set { _runSpeed = RunSpeed; } }
    [SerializeField] Rigidbody _rigidbody;
    private void FixedUpdate()
    {
        _rigidbody.velocity = CustomInputSystem.GetInput();
        
        if (CustomInputSystem.GetInput() != Vector3.zero){

            transform.localRotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }
   
    
    
}
