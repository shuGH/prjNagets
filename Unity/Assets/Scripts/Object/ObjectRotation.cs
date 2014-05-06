using UnityEngine;
using System.Collections;

public class ObjectRotation : MonoBehaviour {

	public Vector3 anglePerSec;
	private TweenRotation tween;

	void Awake () {
		tween = gameObject.GetComponent<TweenRotation>();
		if (tween == null) {
			tween = gameObject.AddComponent<TweenRotation>();
		}
	}

	void Start () {
		StartRotation();
	}
	
	void Update () {
	}

	void StartRotation () {
		if (tween != null) {
			tween.enabled = true;
			tween.onFinished = OnFinishedTween;
			tween.from = gameObject.transform.localEulerAngles;
			tween.to = new Vector3(tween.from.x + anglePerSec.x, tween.from.y + anglePerSec.y, tween.from.z + anglePerSec.z);
			tween.duration = 1f;
			tween.Reset();
			tween.Play(true);
		}
	}

	void OnFinishedTween (UITweener tweener) {
		StartRotation();
	}

	public void EnableRotation (bool isEnable) {
		if (tween != null) {
			tween.enabled = isEnable;
		}
	}
}
