using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

interface IResourceReciever
{
    void OnResourceReceive(ResourceID iD, int resource);
}
public class Finances : MonoBehaviour, IResourceSender, IResourceReciever
{
    private Dictionary<ResourceID, int> _resourcesByID = new Dictionary<ResourceID, int>();
    public int InventoryStatus { get; private set; }
    private IResourceSender _inventorySender;
    public event UnityAction<ResourceID, int> OnResourceChage;

    private void Awake()
    {
        _inventorySender = FindObjectOfType<Inventory>();
        _inventorySender.OnResourceChage += OnResourceReceive;
        _resourcesByID.Add(ResourceID.Money, 0);
    }

    public void OnResourceReceive(ResourceID iD, int resource)
    {
        if (_resourcesByID.ContainsKey(iD))
            _resourcesByID[iD] = resource;
        else _resourcesByID.Add(iD, resource);
    }
    private void OnCollisionEnter(Collision other)
    {
        ISellable objectToSell = other.gameObject.GetComponent<ISellable>();
        if (objectToSell != null)
        {
            _resourcesByID[ResourceID.Money] += objectToSell.Sell();
            OnResourceChage?.Invoke(ResourceID.Money, _resourcesByID[ResourceID.Money]);
        }
    }
    public bool TrySpend(int value)
    {
        if (value <= _resourcesByID[ResourceID.Money])
        {
            _resourcesByID[ResourceID.Money] -= value;
            OnResourceChage?.Invoke(ResourceID.Money, _resourcesByID[ResourceID.Money]);
            return true;
        }
        else return false;
    }

}
