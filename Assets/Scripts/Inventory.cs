using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

interface IResourceSender
{
    event UnityAction<ResourceID, int> OnResourceChage;
}

public class Inventory : MonoBehaviour, IResourceSender, IResourceReciever
{
    [SerializeField] private Transform _layer0, _layer1, _layer2, _layer3, _layer4;
    [SerializeField] private InventoryBrick _inventoryBrickPrefab;
    private Transform _parentBone;
    public event UnityAction<ResourceID, int> OnResourceChage;
    private int _wheetBrickCounter = 0, _inventoryLimit;
    private bool _isInventoryFull;
    public bool IsInventoryFull { get => _isInventoryFull; private set => _isInventoryFull = value; }
    private ISellable[] _bricks;
    private Vector3[] _inventoryGreed;
    private IResourceSender _sender;
    private void Start()
    {
        _inventoryLimit = PlayerPrefs.GetInt("InventoryLimit", 40);
        _bricks = new InventoryBrick[_inventoryLimit];
        _parentBone = _layer0;
        _inventoryGreed = GenerateInventoryGrid();
       // _sender.OnResourceChage += OnResourceReceive;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hangar" && _bricks[0] != null)
        {
            InventoryRemoveAll(other.transform.position);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "WheetBrick" && !IsInventoryFull)
        {
            ICollectible inventoryItem = other.gameObject.GetComponent<ICollectible>();
            inventoryItem.CollectToInventory();
            InventoryAdd(_inventoryBrickPrefab);
        }
    }
    private void InventoryAdd(ISellable inventoryItem)
    {
        GameObject instanted = Instantiate(
            inventoryItem.GetGameObject(),
            _inventoryGreed[_wheetBrickCounter],
            Quaternion.identity);

        instanted.transform.SetParent(_parentBone, false);

        SetParentBone();
        _bricks[_wheetBrickCounter] = instanted.gameObject.GetComponent<ISellable>();
        _wheetBrickCounter++;
        OnResourceChage?.Invoke(ResourceID.InventorySpace, _wheetBrickCounter);
        if (_wheetBrickCounter >= _inventoryLimit)
            IsInventoryFull = true;
    }
    private void InventoryRemoveAll(Vector3 sellerPosition)
    {

        var sequance = DOTween.Sequence();
        for (int i = _wheetBrickCounter - 1; i >= 0; i--)
        {
            sequance.Append(_bricks[i].FlyToSeller(sellerPosition));
            _bricks[i] = null;
        }
        //sequance.OnComplete(() => KillSequence(sequance));

        _wheetBrickCounter = 0;
        OnResourceChage?.Invoke(ResourceID.InventorySpace, _wheetBrickCounter);
        _parentBone = _layer0;
        IsInventoryFull = false;
    }
    async void KillSequence(Sequence sequence){
        await sequence.AsyncWaitForCompletion();
        //sequence.Kill();
    }
    public void OnResourceReceive(ResourceID iD, int value)
    {
        if (iD == ResourceID.InventoryLimit)
            _inventoryLimit = value;
    }
    private Vector3[] GenerateInventoryGrid()
    {
        Vector3[] result = new Vector3[_inventoryLimit];
        float xOffset = 0.0055f, zOffset = 0.01f, yOffset = 0.0032f;

        for (int layer = 0, realLayer = 1, element = 0; layer < _inventoryLimit / 4; layer++, realLayer++)
        {
            if (layer == 4 || layer == 7 || layer == 9 || layer == 10)
                realLayer = 1;

            for (int zPos = 0; zPos < 2; zPos++)
            {
                for (int xPos = 0; xPos < 2; xPos++)
                {
                    if (element == 0)
                    {
                        result[element] = new Vector3(0.003f, 0.003f, 0.006f);
                    }
                    else
                    {
                        result[element] = new Vector3(
                            result[0].x - (xOffset * xPos),
                            yOffset * realLayer,
                            result[0].z - (zOffset * zPos));
                    }
                    element++;
                }
            }
        }
        return result;
    }
    private void SetParentBone()
    {
        switch (_wheetBrickCounter)
        {
            case 15:
                _parentBone = _layer1;
                break;
            case 27:
                _parentBone = _layer2;
                break;
            case 35:
                _parentBone = _layer3;
                break;
            case 39:
                _parentBone = _layer4;
                break;
        }
    }
}
