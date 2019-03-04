using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy")]
    [SerializeField] float enemy_health = 100f;
    [SerializeField] int enemy_points = 150;
    
    [Header("SFX/VFX")]
    [SerializeField] GameObject deathVFXPrefab;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0, 1)] float SFXVolume = 0.7f;

    [Header("Projectile")]
    [SerializeField] GameObject LazerPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 0.8f;


    // Use this for initialization
    void Start () {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update () {
        CountDownAndShoot();
	}

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f) {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject lazer = Instantiate(LazerPrefab, transform.position, Quaternion.identity) as GameObject;
        AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, SFXVolume);
        lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) {
            return;
        }
        DealDamage(collision, damageDealer);
    }

    private void DealDamage(Collider2D collision, DamageDealer damageDealer)
    {
        enemy_health -= damageDealer.getDamage();
        collision.gameObject.GetComponent<DamageDealer>().hit();
        if (enemy_health <= 0) {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        GameSession game = FindObjectOfType<GameSession>();
        game.addToScore(enemy_points);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, SFXVolume);
        GameObject deathVFX = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(deathVFX,1f);
    }
}
