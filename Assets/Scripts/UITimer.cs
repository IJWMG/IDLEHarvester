using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class UITimer : MonoBehaviour
{
    [SerializeField]private Text _text;
    private Image _circleImage;
private void Awake() {
    _circleImage = GetComponent<Image>();
    _circleImage.fillAmount = 0;
}
public void StartTimerFromSeconds(int seconds) => StartCoroutine(FromSecondsTimer(seconds));
private IEnumerator FromSecondsTimer(int time){
    StartCoroutine(ReduseFillAmountFromSeconds(time));
    _text.text = time.ToString();

    for (int i = time -1; i >= 0; i--){
        yield return new WaitForSeconds(1f);
        _text.text = (i).ToString();
    }
        _text.text = "READY";

}
private IEnumerator ReduseFillAmountFromSeconds(int seconds){
    float time = 0;
    _circleImage.fillAmount = 1;
    while(time < (float)seconds){
        _circleImage.fillAmount = Mathf.Lerp(1, 0, time / (float)seconds);
        time += Time.deltaTime;
        yield return new WaitForEndOfFrame();  
    }
    _circleImage.fillAmount = 0;

}
}
