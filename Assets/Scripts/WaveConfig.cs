using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemy_prefab;
    [SerializeField] GameObject path_prefab;
    [SerializeField] float spawn_rate = 0.5f;
    [SerializeField] float spawn_random = 0.3f;
    [SerializeField] int number_enemies = 6;
    [SerializeField] float move_speed = 2f;
    [SerializeField] float wait_bw_waves = 5f;

    public GameObject getenemy_prefab()
    {
        return enemy_prefab;
    }
    public List<Transform> getwaypoints()
    {
        var waypoints = new List<Transform>();
        foreach(Transform v in path_prefab.transform) { 
            waypoints.Add(v);
        }
        return waypoints;
    }
    public float getspawn_rate()
    {
        return spawn_rate;
    }
    public float getspawn_random()
    {
        return spawn_random;
    }
    public int getnumber_enemies()
    {
        return number_enemies;
    }
    public float getmove_speed()
    {
        return move_speed;
    }
    public float get_wait_between_waves()
    {
        return wait_bw_waves;
    }
}
