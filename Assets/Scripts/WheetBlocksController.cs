using UnityEngine;

public class WheetBlocksController : MonoBehaviour
{
    private WheetFeildController controller;
    
    private void OnTriggerEnter(Collider other) {
        controller = this.transform.parent.transform.parent.GetComponent<WheetFeildController>();
        controller.DestroyTheBlock();
        Destroy(this.gameObject);
        Debug.Log("trigger");
    }
}
