using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayScore : MonoBehaviour {

    TextMeshProUGUI scoreText;
    GameSession gamesession;
	// Use this for initialization
	void Start () {
        scoreText = GetComponent<TextMeshProUGUI>();
        gamesession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            scoreText.SetText("Score : " + gamesession.getScore().ToString());
        }
        else {
            scoreText.SetText(gamesession.getScore().ToString());
        }
        
        //scoreText.SetText("100");
    }
}
