using UnityEngine;
using DG.Tweening;
using System.Collections;

   interface ISellable 
{
    int Cost {get; }
    Tween FlyToSeller(Vector3 target);
    int Sell();
    GameObject GetGameObject();

}

public class InventoryBrick : MonoBehaviour, ISellable
{
    public int Cost {get; } = 15;
    public int Sell(){
        transform.DOKill();
        Destroy(this.gameObject);
        return Cost;
    }
    public Tween FlyToSeller(Vector3 target) => this.transform.DOMove(target, 0.1f, false)
            .OnStart(() => this.transform.parent = null); 
    public GameObject GetGameObject() => this.gameObject;

}
