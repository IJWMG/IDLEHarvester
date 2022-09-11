using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICollectible 
{
    Inventory Inventory { get; }
    int Cost {get; }
    void CollectToInventory();
    void StopFollowing();
    int Sell();
    GameObject GetGameObject();
}

public class WheetBrick : MonoBehaviour, ICollectible
{
    public Inventory Inventory { get; set; }
    public int Cost {get; } = 15;
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
    public int Sell(){
        Destroy(this.gameObject);
        return Cost;
    }
}
