using UnityEngine;
using System.Collections;

public class ObjectMoving : MonoBehaviour {

	public Vector3 positionPerSec;
	private TweenPosition tween;

	void Awake () {
		tween = gameObject.GetComponent<TweenPosition>();
		if (tween == null) {
			tween = gameObject.AddComponent<TweenPosition>();
		}
	}

	void Start () {
		StartMoving();
	}

	void Update () {
	}

	void StartMoving () {
		if (tween != null) {
			tween.enabled = true;
			tween.onFinished = OnFinishedTween;
			tween.from = gameObject.transform.localPosition;
			tween.to = new Vector3(tween.from.x + positionPerSec.x, tween.from.y + positionPerSec.y, tween.from.z + positionPerSec.z);
			tween.duration = 1f;
			tween.Reset();
			tween.Play(true);
		}
	}

	void OnFinishedTween (UITweener tweener) {
		StartMoving();
	}

	public void EnableMoving (bool isEnable) {
		if (tween != null) {
			tween.enabled = isEnable;
		}
	}
}
