using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseLifeAnimation : MonoBehaviour{

    public float displacementAmount;

    public float animationDuration;

    public Text lifeIncreaseText;

    public Outline lifeIncreaseOutline;

    public Color lifeIncreaseColor;

    public Color lifeIncreaseOutlineColor;

    private Vector3 _originalPosition;

    private Color _originalTextColor;

    private Color _originalOutlineColor;

    void Awake(){
        _originalTextColor = lifeIncreaseText.color;
        _originalOutlineColor = lifeIncreaseOutline.effectColor;
        _originalPosition = lifeIncreaseText.transform.localPosition;
    }   

    void Start(){
        lifeIncreaseText.enabled = false;
    }


    private void ResetAnimation(){
        lifeIncreaseText.enabled = false;
        lifeIncreaseText.transform.localPosition = _originalPosition;
        lifeIncreaseOutline.effectColor = _originalOutlineColor;
        lifeIncreaseText.color = _originalTextColor;
    }

    public void Animate(){
        StopAllCoroutines();
        ResetAnimation();
        StartCoroutine(AnimateLifeIncrease());
    }

    private IEnumerator AnimateLifeIncrease(){
        float counter = 0f;
        float lerpParam;
        YieldInstruction endOfFrame = new WaitForEndOfFrame();
        Vector3 newPosition = _originalPosition + Vector3.up * displacementAmount;

        lifeIncreaseText.enabled = true;

        while(counter < animationDuration){
            yield return endOfFrame;
            counter += Time.deltaTime;
            lerpParam = counter/ animationDuration;
            lifeIncreaseText.transform.localPosition = Vector3.Lerp(_originalPosition, newPosition, lerpParam);
            lifeIncreaseText.color = Color.Lerp(_originalTextColor, lifeIncreaseColor, lerpParam);
            lifeIncreaseOutline.effectColor = Color.Lerp(_originalOutlineColor, lifeIncreaseOutlineColor, lerpParam);
        }

        lifeIncreaseText.transform.localPosition = newPosition;
        lifeIncreaseText.color = lifeIncreaseColor;
        lifeIncreaseOutline.effectColor = lifeIncreaseOutlineColor;
        yield return endOfFrame;
        ResetAnimation();
    }
}