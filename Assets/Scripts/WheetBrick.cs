using UnityEngine;
using DG.Tweening;

interface ICollectible  
{
    Inventory Inventory { get; }
    void Follow(GameObject player);
    void CollectToInventory();
    void StopFollowing();
}
public class WheetBrick : MonoBehaviour, ICollectible 
{
    public Inventory Inventory { get; set; }
    private void Awake() {
        Inventory = FindObjectOfType<Inventory>();
    }
    public void CollectToInventory(){
        if (!Inventory.IsInventoryFull){
            transform.DOKill();
            Destroy(this.gameObject);
        }
        else StopFollowing();
    }
    public void StopFollowing(){
        transform.DOKill();
    }
    public void Follow(GameObject player){
        transform.DOMove(player.transform.position, 1f, false)
            .OnComplete(() => Follow(player));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !Inventory.IsInventoryFull){
            GameObject player = other.gameObject;
            Follow(player);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player" && Inventory.IsInventoryFull){
            StopFollowing();
        }
    }
}
