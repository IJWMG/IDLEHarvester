using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenUI : MonoBehaviour, IResourceReciever
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    private IResourceSender _financeSender;
    private void Awake() {
        _financeSender = FindObjectOfType<Finances>();
        _financeSender.OnResourceChage += OnResourceReceive;
    }
    private void Start() {
        _moneyText.text = "0 $";

    }
    public void OnResourceReceive(ResourceID iD, int value){
        switch (iD)
        {
            case  ResourceID.Money:
            _moneyText.text = value + " $";
            break;
            default: 
            break;
        }
    }
   
}
