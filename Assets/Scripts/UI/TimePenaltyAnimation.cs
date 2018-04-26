using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePenaltyAnimation : MonoBehaviour {

	public Text text;

	public Outline textOutline;

	public float animationDuration;

	public float displacement;

	public Color textTintColor;

	public Color outlineTintColor;

	private Vector3 _originalPosition;

	private Color _originalTextColor;

	private Color _originalOutlineColor;



	void Awake () {
		_originalPosition = text.transform.localPosition;
		_originalTextColor = text.color;
		_originalOutlineColor = textOutline.effectColor;
	}

	void Start(){
		text.enabled = false;
	}

	private void ResetAnimation(){
		text.enabled = false;
		text.color = _originalTextColor;
		textOutline.effectColor = _originalOutlineColor;
		text.transform.localPosition = _originalPosition;
	}

	public void Animate(){
		StopAllCoroutines();
		ResetAnimation();
		StartCoroutine(AnimatePenalty());
	}

	private IEnumerator AnimatePenalty(){
		float counter = 0f;
		Vector3 newPosition = _originalPosition + displacement * Vector3.up;		
		YieldInstruction endOfFrame = new WaitForEndOfFrame();

		text.enabled = true;

		while(counter < animationDuration){
			yield return endOfFrame;
			counter += Time.deltaTime;
			text.transform.localPosition = Vector3.Lerp(_originalPosition, newPosition, counter / animationDuration);
			text.color = Color.Lerp(_originalTextColor, textTintColor, 2f * counter / animationDuration);
			textOutline.effectColor = Color.Lerp(_originalOutlineColor, outlineTintColor, 2f * counter / animationDuration);
		}

		text.transform.localPosition = newPosition;
		text.color = textTintColor;
		textOutline.effectColor = outlineTintColor;
		yield return endOfFrame;
		ResetAnimation();
	}
}
