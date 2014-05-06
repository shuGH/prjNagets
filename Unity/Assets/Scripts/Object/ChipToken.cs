using UnityEngine;
using System.Collections;

public class ChipToken : GameToken {

	public TextMesh label;

	public int number;

	protected override void Awake () {
		base.Awake();
		type = TokenType.Chip;
	}
	
	// Use this for initialization
	protected override void Start () {
		base.Start();
	
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	
	}

	public void SetupChip (int number) {
		this.number = number;

		if (label != null) label.text = number.ToString("0");
	}
}
