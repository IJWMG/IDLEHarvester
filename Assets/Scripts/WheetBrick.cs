using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WheetBrick : MonoBehaviour, ICollectible
{
    public Inventory Inventory { get; set; }
    private void Awake() {
        Inventory = FindObjectOfType<Inventory>();
    }
    public void CollectToInventory(){
        if (!Inventory.IsInventoryFull){
            Destroy(this.gameObject);
        }
        else StopFollowing();
    }
    public void StopFollowing(){

    }
    public GameObject GetGameObject() => this.gameObject;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !Inventory.IsInventoryFull){
            //возможно юнирх с его апдейтом

        }
    }

}
interface ICollectible 
{
    Inventory Inventory { get; }
    void CollectToInventory();
    void StopFollowing();
    GameObject GetGameObject();
}
