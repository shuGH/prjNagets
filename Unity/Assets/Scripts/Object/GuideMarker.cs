using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuideMarker : MonoBehaviour {
	public enum GuideType {
		None,
		Square,
		SideHorizontal,
		SideVertical
	}

	public enum GuideState {
		None,
		Disabled,
		Enabled,
		Hovered,
		Selected
	}

	[System.Serializable]
	public class MaterialInfo {
		public GuideState state;
		public Material material;
	}

	public GuideType type = GuideType.None;
	public GuideState state = GuideState.None;
	public List<MaterialInfo> materialInfos;

	public Vector2Int posIndex;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetupGuide (GuideType type, Vector2Int posIndex) {
		this.type = type;
		this.posIndex = posIndex;
		this.state = GuideState.Disabled;
	}

	public void SetState (GuideState state) {
		if (materialInfos == null) return;
		if (renderer == null || renderer.material == null) return;

		this.state = state;

		foreach (MaterialInfo info in materialInfos) {
			if (info.state == state) {
				renderer.material = info.material;
				break;
			}
		}
	}
}
