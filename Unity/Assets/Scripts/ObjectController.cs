using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour {
	public enum GameState {
		None,
		Title,
		InGame
	}

	public GameState state = GameState.None;

	public GameObject titleMenu;
	public UIEventListener startButton;
	public GameBoard board;
	public StockMenu stock;

	// Use this for initialization
	void Start () {
		if (startButton != null) startButton.onClick = OnClickedStartButton;

		state = GameState.Title;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == GameState.InGame) {
			if (Input.GetMouseButtonDown(0)) PressAtBoard();
			if (Input.GetMouseButtonUp(0)) ReleaseAtBoard();
		}
	}

	void OnClickedStartButton (GameObject obj) {
		switch (state) {
			case GameState.Title:
				state = GameState.InGame;
				if (board != null) board.SetState(GameBoard.BoardState.InGame);
				if (titleMenu != null) titleMenu.SetActive(false);
				break;
			case GameState.InGame:
				state = GameState.Title;
				if (board != null) board.SetState(GameBoard.BoardState.OutGame);
				if (titleMenu != null) titleMenu.SetActive(true);
				break;
			default:
				break;
		}
	}

	void PressAtBoard () {
		if (board == null || stock == null) return;

		GuideMarker selectedGuide = board.GetActiveGuide();
		if (selectedGuide != null) {
			selectedGuide.SetState(GuideMarker.GuideState.Selected);
			board.isPaused = true;

			if (selectedGuide.type == GuideMarker.GuideType.Square) {
				stock.SetState(StockMenu.StockMenuState.VisibleChip);
			}
			else if (selectedGuide.type == GuideMarker.GuideType.SideHorizontal || selectedGuide.type == GuideMarker.GuideType.SideVertical) {
				stock.SetState(StockMenu.StockMenuState.VisibleBar);
			}
		}
	}

	void ReleaseAtBoard () {
		if (board == null || stock == null) return;

		board.isPaused = false;
		stock.SetState(StockMenu.StockMenuState.Invisible);
		GameToken selectedToken = stock.GetActiveToken();
		GuideMarker selectedGuide = board.GetActiveGuide();
		if (selectedToken != null && selectedGuide != null) {
			stock.MoveTokenToBoard(selectedToken, selectedGuide);
		}
	}
}
