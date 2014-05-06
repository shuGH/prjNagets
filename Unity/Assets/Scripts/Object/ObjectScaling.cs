using UnityEngine;
using System.Collections;

public class ObjectScaling : MonoBehaviour {

	public Vector3 scalePerSec;
	private TweenScale tween;

	void Awake () {
		tween = gameObject.GetComponent<TweenScale>();
		if (tween == null) {
			tween = gameObject.AddComponent<TweenScale>();
		}
	}

	void Start () {
		StartScaling();
	}

	void Update () {
	}

	void StartScaling () {
		if (tween != null) {
			tween.enabled = true;
			tween.onFinished = OnFinishedTween;
			tween.from = gameObject.transform.localScale;
			tween.to = new Vector3(tween.from.x + scalePerSec.x, tween.from.y + scalePerSec.y, tween.from.z + scalePerSec.z);
			tween.duration = 1f;
			tween.Reset();
			tween.Play(true);
		}
	}

	void OnFinishedTween (UITweener tweener) {
		StartScaling();
	}

	public void EnableScaling (bool isEnable) {
		if (tween != null) {
			tween.enabled = isEnable;
		}
	}
}
