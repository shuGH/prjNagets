using UnityEngine;
using System.Collections;

public class GameToken : MonoBehaviour {
	public enum TokenType {
		None,
		Chip,
		Bar
	}

	public enum TokenState {
		None,
		Disabled,
		InStock,
		Hovered,
		InGame
	}

	public TokenType type = TokenType.None;
	public TokenState state = TokenState.None;
	public float hoveredScale;
	private Vector3 defaultScale;

	protected virtual void Awake () {
		defaultScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	protected virtual void Start () {
	}

	protected virtual void Update () {
	}

	public void SetState (TokenState state, bool isIgnoreInGame) {
		if (isIgnoreInGame && this.state == TokenState.InGame) return;

		this.state = state;

		switch (state) {
			case TokenState.Disabled:
				gameObject.SetActive(false);
				transform.localScale = defaultScale;
				//rigidbody.isKinematic = true;
				//collider.isTrigger = true;
				break;
			case TokenState.InStock:
				gameObject.SetActive(true);
				transform.localScale = defaultScale;
				//rigidbody.isKinematic = true;
				//collider.isTrigger = true;
				break;
			case TokenState.Hovered:
				gameObject.SetActive(true);
				transform.localScale = defaultScale * hoveredScale;
				//rigidbody.isKinematic = true;
				//collider.isTrigger = true;
				break;
			case TokenState.InGame:
				gameObject.SetActive(true);
				transform.localScale = defaultScale;
				//rigidbody.isKinematic = false;
				//collider.isTrigger = false;
				break;
			default:
				break;
		}
	}
}
