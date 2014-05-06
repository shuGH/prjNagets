using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour {
	public enum BoardState {
		None,
		OutGame,
		InGame
	}

	public BoardState state = BoardState.None;

	Ray ray;
	RaycastHit hit;

	public GameObject plane;
	public GameObject nagetParent;
	public GameObject ngaetPrefab;
	public GameObject squareGuideParent;
	public GameObject squareGuidePrefab;
	public GameObject sideGuideParent;
	public GameObject sideHorizontalGuidePrefab;
	public GameObject sideVerticalGuidePrefab;
	private List<GuideMarker> guideMarkers;
	private GuideMarker activeMarker;
	private Vector3 defaultRotation;

	// TODO
	public List<NagetToken> nagetTokens;

	public Vector2 gridSize;
	public Vector2Int boardDimension;

	// flag
	public bool isPaused = false;
	private bool isAllGuidesStable = false;

	void Awake () {
		defaultRotation = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
	}

	void Start () {
		SetupBoard();
	}
	
	void Update () {
		if (isPaused) return;

		if (state == BoardState.InGame) {
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			int mask = 1 << NagetsStructUtility.GetLayerInt(GameLayer.Board);
			if (Physics.Raycast(ray, out hit, 10.0f, mask)) {
				GameObject obj = hit.collider.gameObject;
				if (obj != null) {
					GuideMarker guide = obj.GetComponent<GuideMarker>();
					if (guide != null) UpdateMarkers(guide, GuideMarker.GuideState.Hovered);
					else               UpdateMarkers(null, GuideMarker.GuideState.None);
				}
			}
			else {
				UpdateMarkers(null, GuideMarker.GuideState.None);
			}
		}
	}

	void SetupBoard () {
		if (sideGuideParent == null || squareGuideParent == null) return;
		if (sideHorizontalGuidePrefab == null || sideVerticalGuidePrefab == null || squareGuidePrefab == null) return;

		DestoryBoard();
		guideMarkers = new List<GuideMarker>();

		for (int i = 0; i < boardDimension.x; i++) {
			for (int j = 0; j < boardDimension.y; j++) {
				GuideMarker guide = MyUtility.AddChild<GuideMarker>(squareGuideParent, squareGuidePrefab);
				if (guide != null) {
					guide.SetupGuide(GuideMarker.GuideType.Square, new Vector2Int(i,j));
					guide.SetState(GuideMarker.GuideState.Disabled);
					guide.transform.localPosition = new Vector3(i * gridSize.x, 0f, j * gridSize.y);
					guide.name = "square" + i.ToString("0") + j.ToString("0");
					guideMarkers.Add(guide);
				}
			}
		}

		for (int i = 0; i < boardDimension .x- 1; i++) {
			for (int j = 0; j < boardDimension.y; j++) {
				GuideMarker guide = MyUtility.AddChild<GuideMarker>(sideGuideParent, sideHorizontalGuidePrefab);
				if (guide != null) {
					guide.SetupGuide(GuideMarker.GuideType.SideHorizontal, new Vector2Int(i, j));
					guide.SetState(GuideMarker.GuideState.Disabled);
					guide.transform.localPosition = new Vector3(i * gridSize.x, 0f, j * gridSize.y);
					guide.name = "side" + i.ToString("0") + j.ToString("0");
					guideMarkers.Add(guide);
				}
			}
		}
		for (int i = 0; i < boardDimension.x; i++) {
			for (int j = 0; j < boardDimension.y- 1; j++) {
				GuideMarker guide = MyUtility.AddChild<GuideMarker>(sideGuideParent, sideVerticalGuidePrefab);
				if (guide != null) {
					guide.SetupGuide(GuideMarker.GuideType.SideVertical, new Vector2Int(i, j));
					guide.SetState(GuideMarker.GuideState.Disabled);
					guide.transform.localPosition = new Vector3((-0.5f + i) * gridSize.x, 0f, (0.5f + j) * gridSize.y);
					guide.name = "side" + i.ToString("0") + j.ToString("0");
					guideMarkers.Add(guide);
				}
			}
		}

		SetState(state);
	}

	void DestoryBoard () {
		if (guideMarkers == null) return;

		foreach (GuideMarker guide in guideMarkers) {
			Destroy(guide.gameObject);
		}
		guideMarkers.Clear();
	}

	void UpdateMarkers (GuideMarker active, GuideMarker.GuideState guideState) {
		if (guideMarkers == null) return;
		if (isAllGuidesStable && active == null) return;

		activeMarker = active;

		if (active == null) {
			isAllGuidesStable = true;
		}
		else {
			isAllGuidesStable = false;
			active.SetState(guideState);
		}

		foreach (GuideMarker guide in guideMarkers) {
			if (guide != active) {
				guide.SetState(GuideMarker.GuideState.Enabled);
			}
		}
	}

	public void SetState (BoardState state) {
		if (guideMarkers == null) return;

		this.state = state;
		ObjectRotation rotation = gameObject.GetComponent<ObjectRotation>();

		switch (state) {
			case BoardState.OutGame:
				transform.localEulerAngles = defaultRotation;
				if (rotation != null) rotation.EnableRotation(true);

				foreach (GuideMarker guide in guideMarkers) {
					guide.SetState(GuideMarker.GuideState.Disabled);
				}
				foreach (NagetToken naget in nagetTokens) {
					naget.rigidbody.isKinematic = true;
				}
				break;
			case BoardState.InGame:
				transform.localEulerAngles = defaultRotation;
				if (rotation != null) rotation.EnableRotation(false);

				foreach (GuideMarker guide in guideMarkers) {
					guide.SetState(GuideMarker.GuideState.Enabled);
				}
				foreach (NagetToken naget in nagetTokens) {
					naget.rigidbody.isKinematic = false;
				}
				break;
			default:
				break;
		}
	}

	public GuideMarker GetActiveGuide () {
		return activeMarker;
	}
}
