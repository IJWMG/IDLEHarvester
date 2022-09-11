using UnityEngine;
using UnityEngine.Events;

public class Finances : MonoBehaviour, IResourceSender
{
    public int InventoryStatus{ get; private set;}
    public int MoneyCount {get; private set;}
    [SerializeField] private IResourceSender inventorySender;
    public event UnityAction<int> OnResourceChage;

    private void Awake() {
        inventorySender.OnResourceChage += OnResourceReceive;
    }

    public void OnResourceReceive (int resource){
        InventoryStatus = resource;
    }
    private void OnCollisionEnter(Collision other) {
        ICollectible objectToSell = other.gameObject.GetComponent<ICollectible>();
        if (objectToSell != null){
            MoneyCount += objectToSell.Sell();
            OnResourceChage?.Invoke(MoneyCount);
        }
    }
    public bool TrySpend (int value){
        if (value <= MoneyCount){
            MoneyCount -= value;
            OnResourceChage?.Invoke(MoneyCount);
            return true;
        }
        else return false;
    }
}
