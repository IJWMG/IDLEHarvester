using UnityEngine;
using System.Collections;

public class WheetFeildController : MonoBehaviour
{
    [SerializeField] private WheetFieldFragment cellPrefab;
    [SerializeField] private WheetBlocksController _wheetBlockPrefab;
    [SerializeField] private int _xWidth, _zWidth;
    [SerializeField] private int _wheetFieldTimer;
    [SerializeField] private WheetBrick _wheetBrickPrefab;
    [SerializeField] private UITimer _timer;
    private WheetFieldFragment[] _cells;
    private int _destroyCounter;
    void Awake()
    {
        _cells = new WheetFieldFragment[_xWidth * _zWidth];
        for (int z = 0, i = 0; z < _zWidth; z++)
        {
            for (int x = 0; x < _xWidth; x++)
            {
                CreateCell(x, z, i++);
            }
        }
        FilAllCells();
    }

    private void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = x;
        position.y = 0f;
        position.z = z;

        _cells[i] = Instantiate<WheetFieldFragment>(cellPrefab);
        _cells[i].transform.SetParent(transform, false);
        _cells[i].transform.localPosition = position;
    }
    private void FilAllCells()
    {
        // plane as a base of field prefab has scale 10 x 10 x 10, scaler need to match scales of prefabs
        float scaler = cellPrefab.transform.localScale.x * 100f;
        foreach (var cell in _cells)
        {
            Vector3 position = new Vector3(0, _wheetBlockPrefab.transform.localScale.y * scaler / 2f, 0);
            var wheetBlock = Instantiate<WheetBlocksController>(_wheetBlockPrefab);
            wheetBlock.transform.SetParent(cell.transform, false);
            wheetBlock.transform.localPosition = position;
            wheetBlock.transform.localScale *= scaler;
        }
    }
    private bool IsBlockLast()
    {
        bool result = false;
        if (_destroyCounter == (_cells.Length - 1))
        {
            result = true;
        }
        return result;
    }
    public void DestroyTheBlock(Vector3 position)
    {
        if (IsBlockLast())
        {
            _destroyCounter = 0;
            StartCoroutine(ReloadWheetField());
        }
        else { _destroyCounter++; }
        Instantiate(_wheetBrickPrefab, position, Quaternion.identity);
    }
    private IEnumerator ReloadWheetField()
    {
        _timer.StartTimerFromSeconds(_wheetFieldTimer);
        yield return new WaitForSeconds(_wheetFieldTimer);
        FilAllCells();
    }
}
