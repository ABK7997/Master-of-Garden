using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

    public Text small, med, large, mountains, zen;

	// Use this for initialization
	void Start () {
        ScoreManager.load();

        small.text = "Best Day Number: " + ScoreManager.smallDays + "\n" +
            "Best Score: " + ScoreManager.smallScore;

        med.text = "Best Day Number: " + ScoreManager.medDays + "\n" +
            "Best Score: " + ScoreManager.medScore;

        large.text = "Best Day Number: " + ScoreManager.largeDays + "\n" +
            "Best Score: " + ScoreManager.largeScore;

        mountains.text = "Best Day Number: " + ScoreManager.mountainDays + "\n" +
            "Best Score: " + ScoreManager.mountainScore;

        zen.text = "Best Day Number: " + ScoreManager.zenDays + "\n" +
            "Best Score: " + ScoreManager.zenScore;
    }
}
