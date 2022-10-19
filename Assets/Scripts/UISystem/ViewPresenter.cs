using UnityEngine;

interface IResourceViewer
{
    void DisplayResourceOnUI(ResourceID iD, int value);
}
public class ViewPresenter : MonoBehaviour, IResourceReciever
{
    [SerializeField] private ScreenUI _screenUI;
    [SerializeField] private ShopUI _shopUI;
    [SerializeField] private InventoryUI _inventoryUI;
    private Finances _finances;
    private Shop _shop;
    private void Awake()
    {
        _shop = FindObjectOfType<Shop>();
        _shop.OnTradeDisabled += DisableTrade;
        _finances = FindObjectOfType<Finances>();
        _finances.OnResourceChage += OnResourceReceive;

    }
    private void DisableTrade(ResourceID iD){
       _shopUI.DisableTrade(iD);
    }
    public void OnResourceReceive(ResourceID iD, int resource)
    {
         switch (iD)
        {
            case ResourceID.ActiveFeilds:
                _screenUI.DisplayResourceOnUI(iD, resource);
                break;
            case ResourceID.FeildUpdatePrice:
                _shopUI.DisplayResourceOnUI(iD, resource);
                break;
            case ResourceID.InventoryLimit:
                _inventoryUI.DisplayResourceOnUI(iD, resource);
                break;
            case ResourceID.InventorySpace:
                _inventoryUI.DisplayResourceOnUI(iD, resource);
                break;
            case ResourceID.InventoryUpgradePrice:
                _shopUI.DisplayResourceOnUI(iD, resource);
                break;
            case ResourceID.Money:
                _screenUI.DisplayResourceOnUI(iD, resource);
                break;
            case ResourceID.SpeedBoostPrice:
                _shopUI.DisplayResourceOnUI(iD, resource);
                break;
        }
    }
}
