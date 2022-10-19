using UnityEngine;

public class FeildStatusController : MonoBehaviour
{
    [SerializeField] private WheetFeildController[] _wheetFeilds = new WheetFeildController[10];
    private bool _firstFrame = true;
    private int _activeFeildCount;
    public int ActiveFeildCount
    {
        get { return _activeFeildCount; }
        set
        {
            if (value < _wheetFeilds.Length && value > 0)
            {
                _activeFeildCount = value;
            }
        }
    }
    private void FixedUpdate()
    {
        if (_firstFrame){
        _firstFrame = false;
        InitializeFeilds();
        }
    }
    public bool TryAddActiveFeild()
    {
        if (ActiveFeildCount + 1 >= _wheetFeilds.Length){
            return false;
        }
        return true;

    }
    public void AddActiveFeild(){
        ActiveFeildCount++;
        OpenNewFeild();

    }

    private void OpenNewFeild() => _wheetFeilds[ActiveFeildCount].gameObject.SetActive(true);

    private void InitializeFeilds()
    {
        for (int i = 0; i <= ActiveFeildCount; i++)
        {
            _wheetFeilds[i].gameObject.SetActive(true);

        }
    }
    public bool IsAllFeildActive(){
        if (ActiveFeildCount == _wheetFeilds.Length -1)
        {
            return true;
        }
        else return false;
    }

}
