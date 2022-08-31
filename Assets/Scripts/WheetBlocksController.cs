using UnityEngine;

public class WheetBlocksController : MonoBehaviour
{
    private WheetFeildController controller;
    private bool isExist = true;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Scythe") && isExist)
        {
            isExist = false;
            controller = this.transform.parent.transform.parent.GetComponent<WheetFeildController>();
            controller.DestroyTheBlock(this.transform.position);
            Destroy(this.gameObject, 0.25f);
            Debug.Log("trigger");
        }
    }
}
