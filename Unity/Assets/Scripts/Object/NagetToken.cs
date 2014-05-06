using UnityEngine;
using System.Collections;

public class NagetToken : MonoBehaviour {
	public TextMesh label;

	public int number;

	void Awake () {
	}
	
	// Use this for initialization
	void Start () {
		MyUtility.AddRandomRotate(transform, 0f, 20f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetupChip (int number) {
		this.number = number;

		if (label != null) label.text = number.ToString("0");
	}
}
