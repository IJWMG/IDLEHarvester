using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenUI : MonoBehaviour, IResourceViewer
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    private void Awake()
    {
    }
    private void Start()
    {
        _moneyText.text = "0 $";

    }
    public void DisplayResourceOnUI(ResourceID iD, int value)
    {
        if (iD == ResourceID.Money)
        {
            _moneyText.text = value + " $";
        }
    }


}
