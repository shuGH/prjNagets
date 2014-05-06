using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StockMenu : MonoBehaviour {
	public enum StockMenuState {
		None,
		Invisible,
		VisibleChip,
		VisibleBar
	}

	public StockMenuState state = StockMenuState.None;

	public Camera targetCamera;
	Ray ray;
	RaycastHit hit;

	public GameObject plane;
	public GameObject chipParent;
	public GameObject chipPrefab;
	public GameObject barParent;
	public GameObject barPrefab;
	private List<GameToken> gameTokens;
	private GameToken activeToken;

	public List<int> chipNumbers;
	public int barCount = 37;

	public Vector2 gridSizeChip;
	public Vector2 gridSizeBar;
	public int columnCountChip = 6;
	public int columnCountBar = 6;

	public GameObject boardChipParent;
	public GameObject boardBarParent;

	// flag
	public bool isPaused = false;
	private bool isAllTokensStable = false;

	// Use this for initialization
	void Start () {
		SetupStock();
		// transform.LookAt(Camera.main.transform.position);
		//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - 90f, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (isPaused) return;

		if (state == StockMenuState.VisibleChip || state == StockMenuState.VisibleBar) {
			ray = targetCamera.ScreenPointToRay(Input.mousePosition);
			int mask = 1 << NagetsStructUtility.GetLayerInt(GameLayer.Stock);
			if (Physics.Raycast(ray, out hit, 10.0f, mask)) {
				GameObject obj = hit.collider.gameObject;
				if (obj != null) {
					GameToken token = obj.GetComponent<GameToken>();
					if (token != null) UpdateTokens(token, GameToken.TokenState.Hovered);
					else UpdateTokens(null, GameToken.TokenState.None);
				}
			}
			else {
				UpdateTokens(null, GameToken.TokenState.None);
			}
		}
	}

	void SetupStock () {
		if (chipParent == null || chipPrefab == null) return;
		if (barParent == null || barPrefab == null) return;

		this.state = StockMenuState.Invisible;

		DestoryStock();
		gameTokens = new List<GameToken>();
		MyUtility.ShuffleList<int>(chipNumbers);

		for (int i = 0; i < chipNumbers.Count; i++) {
			ChipToken chip = MyUtility.AddChild<ChipToken>(chipParent, chipPrefab);
			if (chip != null) {
				chip.SetupChip(chipNumbers[i]);
				chip.SetState(GameToken.TokenState.Disabled, false);
				chip.transform.localPosition = GetTokenPosition(i, columnCountChip, chipNumbers.Count-1, gridSizeChip);
				chip.name = "chip" + chipNumbers[i].ToString("0");
				MyUtility.AddRandomPosition(chip.transform, 0.4f, 0f, 0.4f);
				MyUtility.AddRandomRotate(chip.transform, 0f, 30f, 0f);
				gameTokens.Add(chip);
			}
		}
		for (int i = 0; i < barCount; i++) {
			BarToken bar = MyUtility.AddChild<BarToken>(barParent, barPrefab);
			if (bar != null) {
				bar.SetupBar(BarToken.BarType.Vertical);
				bar.SetState(GameToken.TokenState.Disabled, false);
				bar.transform.localPosition = GetTokenPosition(i, columnCountBar, barCount - 1, gridSizeBar);
				bar.name = "bar" + i.ToString("0");
				MyUtility.AddRandomPosition(bar.transform, 0.4f, 0f, 0.4f);
				MyUtility.AddRandomRotate(bar.transform, 0f, 20f, 0f);
				gameTokens.Add(bar);
			}
		}

		if (plane !=null) plane.SetActive(false);

		SetState(state);
	}

	void DestoryStock () {
		if (gameTokens == null) return;

		foreach (GameToken token in gameTokens) {
			Destroy(token.gameObject);
		}
		gameTokens.Clear();
	}

	Vector3 GetTokenPosition (int index, int columnCount, int maxIndex, Vector2 size) {
		if (columnCount == 0) return Vector3.zero;

		Vector3 vec = Vector3.zero;
		int r = index / columnCount;
		int c = index % columnCount;
		int maxR = maxIndex / columnCount;
		int lastC = (maxIndex % columnCount);
		int cnt = (r != maxR) ? columnCount-1 : lastC;
		vec.x = ((float)c - ((float)(cnt) / 2f)) * size.x;
		vec.z = ((float)r - ((float)(maxR) / 2f)) * size.y;
	
		return vec;
	}

	void UpdateTokens (GameToken active, GameToken.TokenState tokenState) {
		if (gameTokens == null) return;
		if (isAllTokensStable && active == null) return;

		activeToken = active;

		if (active == null) {
			isAllTokensStable = true;
		}
		else {
			isAllTokensStable = false;
			active.SetState(tokenState, true);
		}

		foreach (GameToken token in gameTokens) {
			if (token != active) {
				if (token.type == GameToken.TokenType.Chip && state == StockMenuState.VisibleChip) {
					token.SetState(GameToken.TokenState.InStock, true);
				}
				else if (token.type == GameToken.TokenType.Bar && state == StockMenuState.VisibleBar) {
					token.SetState(GameToken.TokenState.InStock, true);
				}
			}
		}
	}

	public void SetState (StockMenuState state) {
		if (gameTokens == null) return;

		this.state = state;
		switch (state) {
			case StockMenuState.Invisible:
				foreach (GameToken token in gameTokens) {
					token.SetState(GameToken.TokenState.Disabled, true);
				}
				if (plane !=null) plane.SetActive(false);
				break;
			case StockMenuState.VisibleChip:
				foreach (GameToken token in gameTokens) {
					if (token.type == GameToken.TokenType.Chip) token.SetState(GameToken.TokenState.InStock, true);
					else token.SetState(GameToken.TokenState.Disabled, true);
				}
				if (plane !=null) plane.SetActive(true);
				break;
			case StockMenuState.VisibleBar:
				foreach (GameToken token in gameTokens) {
					if (token.type == GameToken.TokenType.Bar) token.SetState(GameToken.TokenState.InStock, true);
					else token.SetState(GameToken.TokenState.Disabled, true);
				}
				if (plane !=null) plane.SetActive(true);
				break;
			default:
				break;
		}
	}

	public GameToken GetActiveToken () {
		return activeToken;
	}

	public void MoveTokenToBoard (GameToken token, GuideMarker guide) {
		if (token == null || guide == null) return;

		token.SetState(GameToken.TokenState.InGame, false);
		if (token.type == GameToken.TokenType.Chip) token.transform.parent = boardChipParent.transform;
		else if (token.type == GameToken.TokenType.Bar) {
			BarToken bar = token.GetComponent<BarToken>();
			if (bar != null) {
				if (guide.type == GuideMarker.GuideType.SideVertical) {
					bar.SetupBar(BarToken.BarType.Vertical);
				}
				else if (guide.type == GuideMarker.GuideType.SideHorizontal) {
					bar.SetupBar(BarToken.BarType.Horizontal);
				}
			}
			token.transform.parent = boardBarParent.transform;
		}
		MyUtility.SetLayerRecursive(guide.gameObject.layer, token.gameObject);
		token.gameObject.transform.localPosition = guide.gameObject.transform.localPosition;
	}
}
