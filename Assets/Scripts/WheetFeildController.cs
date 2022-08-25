using UnityEngine;
using System.Collections;

public class WheetFeildController : MonoBehaviour
{
    [SerializeField] private WheetFieldFragment cellPrefab;
    [SerializeField] private WheetBlocksController wheetBlockPrefab;
    [SerializeField] private int xWidth, zWidth;
    private WheetFieldFragment[] cells;
    private int destroyCounter;
    void Awake()
    {
        cells = new WheetFieldFragment[xWidth * zWidth];
        for (int z = 0, i = 0; z < zWidth; z++)
        {
            for (int x = 0; x < xWidth; x++)
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

        cells[i] = Instantiate<WheetFieldFragment>(cellPrefab);
        cells[i].transform.SetParent(transform, false);
        cells[i].transform.localPosition = position;
    }
    private void FilAllCells()
    {
        // plane as a base of field prefab has scale 10 x 10 x 10, scaler need to match scales of prefabs
        float scaler = cellPrefab.transform.localScale.x * 100f;
        foreach (var cell in cells)
        {
            Vector3 position = new Vector3(0, wheetBlockPrefab.transform.localScale.y * scaler / 2f, 0);
            var wheetBlock = Instantiate<WheetBlocksController>(wheetBlockPrefab);
            wheetBlock.transform.SetParent(cell.transform, false);
            wheetBlock.transform.localPosition = position;
            wheetBlock.transform.localScale *= scaler;
        }
    }
    private bool IsBlockLast()
    {
        bool result = false;
        if (destroyCounter == (cells.Length -1)){
            result = true;
        }
        return result;
    }
    public void DestroyTheBlock(){
        Debug.Log(destroyCounter);
        if (IsBlockLast()){
            destroyCounter = 0;
            StartCoroutine(ReloadWheetField());
        }
        else{destroyCounter++;}
    }
    private IEnumerator ReloadWheetField(){
        yield return new WaitForSeconds (3f);
        FilAllCells();
    }
}
