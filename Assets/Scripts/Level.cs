using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class Level : MonoBehaviour {

    [SerializeField] float delaySeconds = 2f;
    [SerializeField] GameObject lazer;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void GameOver()
    {
        /*if(FindObjectsOfType<>().Length <= 0) {
            SceneManager.LoadScene("Game Over");
        }*/

        StartCoroutine(WaitingAWhile());
    }

    private IEnumerator WaitingAWhile()
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("Game Over");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
