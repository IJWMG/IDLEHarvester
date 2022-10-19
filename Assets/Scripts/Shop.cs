using UnityEngine;
using UnityEngine.Events;


public class Shop : MonoBehaviour, IResourceSender, IResourceReciever
{
    [SerializeField] private ShopTrigger[] _triggers;
    [SerializeField] private Finances _finances;
    public event UnityAction<ResourceID, int> OnResourceChage;
    public event UnityAction<ResourceID> OnTradeDisabled;
    private int _feildUpgradePrice, _inventoryUpgradePrice, _speedBoostPrice;
    private int _activeFeildCount = 1;
    private FeildStatusController _feildStatus;
    private void Awake()
    {
        _feildStatus = GetComponent<FeildStatusController>();
        _finances.OnResourceChage += OnResourceReceive;

    }
    private void Start()
    {
        foreach (var trigger in _triggers)
        {
            trigger.OnShopTriggerEnter += ShopTriggerEnter;
        }
        //TODO сделать проверку на возможность покупки на старте игры
    }
    private void ShopTriggerEnter(string tag, ShopTrigger currentTrigger)
    {
        switch (tag)
        {
            case "WheetFeildTrigger":
                BuyNewField(currentTrigger);
                break;
            case "InventoryTrigger":
                BuyMoreInventorySpace(currentTrigger);
                break;
            case "SpeedTrigger":
                BuySpeedBoost(currentTrigger);
                break;

        };
    }
    private async void BuySpeedBoost(ShopTrigger trigger){
        if (_finances.TrySpend(_speedBoostPrice)){
            PlayerController.RunSpeed = 10f;
            await trigger.TemporaryDisable();
            PlayerController.RunSpeed = 5.5f;
        }
    }
    private void BuyMoreInventorySpace(ShopTrigger trigger)
    {
        if ((_inventoryUpgradePrice / 100) + 20 > Inventory.MAX_INVENTORY_LIMIT){
            trigger.DisableTrade();
            OnTradeDisabled?.Invoke(ResourceID.InventoryUpgradePrice);
            return;
        }
        if (_finances.TrySpend(_inventoryUpgradePrice)){
            IncreaseInventoryUpgradePrice();
            OnResourceChage?.Invoke(ResourceID.InventoryLimit, ((_inventoryUpgradePrice / 100) + 10));
        }
    }
    private void IncreaseInventoryUpgradePrice(){
        _inventoryUpgradePrice += 1000;
        OnResourceChage?.Invoke(ResourceID.InventoryUpgradePrice, _inventoryUpgradePrice);
        print("inv price is " + _inventoryUpgradePrice);
    }


    private void BuyNewField(ShopTrigger trigger)
    {
        if (!_feildStatus.TryAddActiveFeild()){
            trigger.DisableTrade();
            OnTradeDisabled?.Invoke(ResourceID.FeildUpdatePrice);

            return;
        }
        if (_finances.TrySpend(_feildUpgradePrice))
        {
            _feildStatus.AddActiveFeild();
            _activeFeildCount++;
            IncreaseFeildPrice();
            OnResourceChage?.Invoke(ResourceID.ActiveFeilds, _activeFeildCount);
        }
    }
    private void IncreaseFeildPrice()
    {
        if (_activeFeildCount < 4)
        {
            _feildUpgradePrice += 500;
        }
        else if (_activeFeildCount == 4)
        {
            _feildUpgradePrice += 1000;
        }
        else
        {
            _feildUpgradePrice *= 2;
        }
        OnResourceChage?.Invoke(ResourceID.FeildUpdatePrice, _feildUpgradePrice);
        print("Feild Price is " + _feildUpgradePrice);
    }



    public void OnResourceReceive(ResourceID iD, int resource)
    {
        switch (iD)
        {
            case ResourceID.ActiveFeilds:
                _activeFeildCount = resource;
                _feildStatus.ActiveFeildCount = resource;
                break;
            case ResourceID.FeildUpdatePrice:
                _feildUpgradePrice = resource;
                break;
            case ResourceID.InventoryUpgradePrice:
                _inventoryUpgradePrice = resource;
                break;
            case ResourceID.SpeedBoostPrice:
                _speedBoostPrice = resource;
                break;
        }
    }

}
