using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    void LoadResources();
}

public interface IUserAction
{
	int ShowScore();			//结束界面显示分数
	void gameStart();
    void gameOver();
}
