using UnityEngine;
using DG.Tweening;

interface ICollectible  
{
    Inventory Inventory { get; }
    void Follow(Vector3 position);
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
    public void Follow(Vector3 position){
        transform.DOMove(position, 0.5f, false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !Inventory.IsInventoryFull){
            Follow(other.transform.position);
        }
    }
}
