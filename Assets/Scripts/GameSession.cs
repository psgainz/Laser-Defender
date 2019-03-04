using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    int score = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int getScore()
    {
        return score;
    }

    public void addToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void resetGame()
    {
        Destroy(gameObject);
    }

}
