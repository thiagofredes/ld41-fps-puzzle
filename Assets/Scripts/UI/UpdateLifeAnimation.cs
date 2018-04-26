using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLifeAnimation : MonoBehaviour{

    public float displacementAmount;

    public float animationDuration;

    public Text lifeUpdateText;

    public Outline lifeUpdateOutline;

    public Color lifeIncreaseColor;

    public Color lifeIncreaseOutlineColor;
    
    public Color lifeDecreaseColor;

    public Color lifeDecreaseOutlineColor;

    private Vector3 _originalPosition;

    private Color _originalTextColor;

    private Color _originalOutlineColor;

    void Awake(){
        _originalTextColor = lifeUpdateText.color;
        _originalOutlineColor = lifeUpdateOutline.effectColor;
        _originalPosition = lifeUpdateText.transform.localPosition;
    }   

    void Start(){
        lifeUpdateText.enabled = false;
    }

    private void ResetAnimation(){
        lifeUpdateText.enabled = false;
        lifeUpdateText.text = "";
        lifeUpdateText.transform.localPosition = _originalPosition;
        lifeUpdateOutline.effectColor = _originalOutlineColor;
        lifeUpdateText.color = _originalTextColor;
    }

    public void AnimateIncrease(){
        StopAllCoroutines();
        ResetAnimation();
        StartCoroutine(AnimateLifeUpdate("+1",
                                            _originalPosition,
                                            _originalPosition - Vector3.up * displacementAmount,
                                            lifeIncreaseColor,
                                            lifeIncreaseOutlineColor));
    }

    public void AnimateDecrease(){
        StopAllCoroutines();
        ResetAnimation();
        StartCoroutine(AnimateLifeUpdate("-1",
                                            _originalPosition - Vector3.up * displacementAmount,
                                            _originalPosition,
                                            lifeDecreaseColor,
                                            lifeDecreaseOutlineColor));
    }

    private IEnumerator AnimateLifeUpdate(string text, Vector3 startPosition, Vector3 finalPosition, Color finalTextColor, Color finalOutlineColor){
        float counter = 0f;
        float lerpParam;
        YieldInstruction endOfFrame = new WaitForEndOfFrame();

        lifeUpdateText.text = text;
        lifeUpdateText.enabled = true;

        while(counter < animationDuration){
            yield return endOfFrame;
            counter += Time.deltaTime;
            lerpParam = counter/ animationDuration;
            lifeUpdateText.transform.localPosition = Vector3.Lerp(startPosition, finalPosition, lerpParam);
            lifeUpdateText.color = Color.Lerp(_originalTextColor, finalTextColor, lerpParam);
            lifeUpdateOutline.effectColor = Color.Lerp(_originalOutlineColor, finalOutlineColor, lerpParam);
        }

        lifeUpdateText.transform.localPosition = finalPosition;
        lifeUpdateText.color = finalTextColor;
        lifeUpdateOutline.effectColor = finalOutlineColor;
        yield return endOfFrame;
        ResetAnimation();
    }
}