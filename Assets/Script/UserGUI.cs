using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
	private IUserAction action;
    public int life = 5;
    private bool gameStart = false;
	void Start() {
		action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
	}
	void OnGUI () {
		GUIStyle textStyle = new GUIStyle();
		GUIStyle scoreStyle = new GUIStyle();
		GUIStyle lifeStyle = new GUIStyle();
		GUIStyle other = new GUIStyle();
        textStyle.normal.textColor = Color.black;
        textStyle.fontSize = 20;
		scoreStyle.normal.textColor = Color.yellow;
        scoreStyle.fontSize = 20;
        lifeStyle.normal.textColor = Color.red;
        lifeStyle.fontSize = 20;
		other.fontSize = 35;


		if (gameStart) {
			GUI.Label(new Rect(10, 5, 200, 50), "分数:", textStyle);
            GUI.Label(new Rect(70, 5, 200, 50), action.ShowScore().ToString(), scoreStyle);
            GUI.Label(new Rect(10, 20, 50, 50), "生命:", textStyle);
            for (int i = 0; i < life; i++) {
                GUI.Label(new Rect(60 + 10 * i, 20, 50, 50), "♥", lifeStyle);
            }

			// 生命归0则结束游戏
			if (life == 0) {
			   _gameOver();
			   action.gameOver();
			   GUI.Label(new Rect(Screen.width / 2 - 20, Screen.width / 2 - 250, 100, 100), "游戏结束",other);
			   GUI.Label(new Rect(Screen.width / 2 - 10, Screen.width / 2 - 200, 50, 50), "得分:", scoreStyle);
			   GUI.Label(new Rect(Screen.width / 2 + 50, Screen.width / 2 - 200, 50, 50),action.ShowScore().ToString(), textStyle);
		}
	}
	GUI.Label(new Rect((Screen.width-20)*0.5f, Screen.height*0.5f-100, 100, 100), "Hit UFO", other);
	if (!gameStart) {
		if (GUI.Button(new Rect((Screen.width-20)*0.5f, (Screen.height-50)*0.5f, 100, 50), "游戏开始")) {
	                gameStart = true;
					life = 5;
	                action.gameStart();
	        }
	}
	}
	public void MissUFO() {
		if(life > 0)	life--;
	}
	public void _gameOver() {
		gameStart = false;
	}
}
