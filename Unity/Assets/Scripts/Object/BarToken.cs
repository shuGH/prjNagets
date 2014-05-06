using UnityEngine;
using System.Collections;

public class BarToken : GameToken {
	public enum BarType {
		None,
		Horizontal,
		Vertical
	}

	public BarType barType = BarType.None;

	protected override void Awake () {
		base.Awake();
		type = TokenType.Bar;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();

	}

	// Update is called once per frame
	protected override void Update () {
		base.Update();

	}

	public void SetupBar (BarType barType) {
		this.barType = barType;
		if (barType == BarType.Horizontal) {
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 90f, transform.localEulerAngles.z);
		}
		else if (barType == BarType.Vertical) {
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}
	}
}
