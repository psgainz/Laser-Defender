using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveconfigs;
    int starting_wave = 0;
    [SerializeField] bool looping = true;
    
	// Use this for initialization
	IEnumerator Start () {
        do {
            yield return StartCoroutine(spawnAllWaves());
        }
        while (looping);
	}

    private IEnumerator spawnAllWaves()
    {
        for (var waveIndex = starting_wave; waveIndex < waveconfigs.Count; waveIndex++) {
            var current_wave = waveconfigs[waveIndex];
            yield return StartCoroutine(spawnAllEnemiesInWave(current_wave));
        }
    }

    private IEnumerator spawnAllEnemiesInWave(WaveConfig current_waveConfig)
    {
        for (var enemyCount = 0; enemyCount < current_waveConfig.getnumber_enemies(); enemyCount++) {
            var enemy = Instantiate(current_waveConfig.getenemy_prefab(), current_waveConfig.getwaypoints()[0].transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().setWaveConfig(current_waveConfig);
            yield return new WaitForSeconds(current_waveConfig.getspawn_rate());
        }
        yield return new WaitForSeconds(current_waveConfig.get_wait_between_waves());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
