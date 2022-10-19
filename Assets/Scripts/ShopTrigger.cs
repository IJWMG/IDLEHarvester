using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using DG.Tweening;


public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private Renderer _renderer;
    public event UnityAction<string, ShopTrigger> OnShopTriggerEnter;
    private Color _firstColor;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        transform.DOMoveY(1.5f, 1.5f, false).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 180, 0), _rotationSpeed).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart) ;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            OnShopTriggerEnter?.Invoke(this.tag, this);
            CreateParticles();
        }
    }
    public void DisableTrade()
    {
        _firstColor = _renderer.material.color;
        _renderer.material.SetColor("_Color", Color.gray);
        GetComponent<BoxCollider>().enabled = false;
    }
    private void EnableTrade()
    {
        _renderer.material.color = _firstColor;
        GetComponent<BoxCollider>().enabled = true;
    }
    public async Task TemporaryDisable()
    {
        DisableTrade();
        await Task.Delay(10000);
        EnableTrade();
    }
    private void CreateParticles(){
        //TODO made some VFX
    }
}
