using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static float runSpeed = 0.1f;
    public static float RunSpeed { get { return runSpeed; } private set { runSpeed = RunSpeed; } }
    private Animator animator;
    [SerializeField] private ScytheScript scythePrefab;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        transform.Translate(CustomInputSystem.GetInput());
        if (Input.GetKeyDown(KeyCode.E)){
            animator.SetBool("isCutting", true);
        }
    }
    private void OnTriggerEnter(Collider other) {
       /* Instantiate(
            scythePrefab, 
            new Vector3(
                scythePrefab.transform.position.x, 
                scythePrefab.transform.position.y, 
                scythePrefab.transform.position.z
                ), 
            Quaternion.identity, 
            GameObject.FindGameObjectWithTag("RightHand").transform
        );
        */
        //animator.SetBool("isCutting", true);
       
    }
    
    
}
