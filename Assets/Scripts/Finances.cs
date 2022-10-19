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
    private IResourceSender _inventorySender, _shopSender;
    public event UnityAction<ResourceID, int> OnResourceChage;

    private void Awake()
    {
        _inventorySender = FindObjectOfType<Inventory>();
        _inventorySender.OnResourceChage += OnResourceReceive;
        _shopSender = FindObjectOfType<Shop>();
        _shopSender.OnResourceChage += OnResourceReceive;
        _resourcesByID = DataSaveLoader.LoadAllData();

    }
    private void Start()
    {
        foreach (var resource in _resourcesByID)
        {
            OnResourceChage?.Invoke(resource.Key, resource.Value);

        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            _resourcesByID = DataSaveLoader.LoadAllData();
            foreach (var resource in _resourcesByID)
            {
                OnResourceChage?.Invoke(resource.Key, resource.Value);
            }
            print(_resourcesByID);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DataSaveLoader.SaveAllData(_resourcesByID);
        }
        if (Input.GetKeyDown(KeyCode.M)){
            _resourcesByID[ResourceID.Money] = 1000000000;
            OnResourceChage?.Invoke(ResourceID.Money, _resourcesByID[ResourceID.Money]);
            print("cheats on");
        }
    }


    public void OnResourceReceive(ResourceID iD, int resource)
    {
        if (_resourcesByID.ContainsKey(iD))
        {
            _resourcesByID[iD] = resource;
        }
        else { _resourcesByID.Add(iD, resource); }
        OnResourceChage?.Invoke(iD, resource);
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
    private void OnApplicationQuit()
    {
        //DataSaveLoader.SaveAllData(_resourcesByID);
    }
}
