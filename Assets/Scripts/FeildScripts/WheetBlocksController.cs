using UnityEngine;

public class WheetBlocksController : MonoBehaviour
{
    private WheetFeildController _controller;
    private bool _isExist = true;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Scythe") && _isExist)
        {
            _isExist = false;
            _controller = this.transform.parent.transform.parent.GetComponent<WheetFeildController>();
            _controller.DestroyTheBlock(this.transform.position);
            Destroy(this.gameObject, 0.25f);
        }
    }
}
