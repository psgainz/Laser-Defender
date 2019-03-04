using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHealth : MonoBehaviour {

    [SerializeField] Player player;
    [SerializeField] float barWidth = 5f;
    float player_health = 0f;
    int max_player_health;
    float width;

    // Use this for initialization
    void Start () {
        max_player_health = player.getHealth();
        player_health = max_player_health;
        width = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        player_health = player.getHealth();
        width = player_health / max_player_health;
        transform.localScale = new Vector2(width,transform.localScale.y);
    }
}
