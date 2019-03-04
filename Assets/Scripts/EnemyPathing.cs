using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    WaveConfig waveconfig;
    List<Transform> waypoints;
    float moveSpeed;

    int waypoint_index = 0;
    
	// Use this for initialization
	void Start () {
        moveSpeed = waveconfig.getmove_speed();
        waypoints = waveconfig.getwaypoints();
        transform.position = waypoints[waypoint_index].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Path();
    }

    public void setWaveConfig(WaveConfig waveConfig)
    {
        this.waveconfig = waveConfig;
    }

    private void Path()
    {
        if (waypoint_index <= waypoints.Count - 1) {
            var target_position = waypoints[waypoint_index].transform.position;
            var frame_moveSpeed = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target_position, frame_moveSpeed);
            if (transform.position == target_position) {
                waypoint_index++;
            }
        }
        else {
            waypoint_index = 0;
        }
    }
}
