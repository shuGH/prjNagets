using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameLayer {
	Default,
	UI,
	Board,
	Stock,
	Effect
}

public class NagetsStructUtility {
	public static int GetLayerInt (GameLayer layer) {
		switch (layer) {
			case GameLayer.Default: return LayerMask.NameToLayer("Default");
			case GameLayer.UI: return LayerMask.NameToLayer("UI");
			case GameLayer.Board: return LayerMask.NameToLayer("Board");
			case GameLayer.Stock: return LayerMask.NameToLayer("Stock");
			case GameLayer.Effect: return LayerMask.NameToLayer("Effect");
			default: return LayerMask.NameToLayer("Default");
		}
	}
}