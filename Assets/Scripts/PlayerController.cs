using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static float _runSpeed = 0.1f;
    public static float RunSpeed { get { return _runSpeed; } private set { _runSpeed = RunSpeed; } }
    [SerializeField] private ScytheScript scythePrefab;
    private void FixedUpdate()
    {
        transform.Translate(CustomInputSystem.GetInput());
    }
   
    
    
}
