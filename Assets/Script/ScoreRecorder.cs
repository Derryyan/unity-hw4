using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {
    public int score;
	private int scorePerDisk = 10;
    void Start () {
        score = 0;
    }
    public void Record() {
        score += scorePerDisk;
    }
    public void Reset() {
        score = 0;
    }
}
