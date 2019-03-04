using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //config params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = 1f;
    [SerializeField] float yPadding = 1f;
    [SerializeField] int player_health = 1000;

    [Header("SFX/VFX")]
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0, 1)] float SFXVolume = 0.75f;
    [SerializeField] GameObject deathVFXPrefab;
         
    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float firingGap = 0.1f;
    [SerializeField] GameObject LazerPrefab;
     
    float xMin, xMax, yMin, yMax;
    Coroutine firingCoroutine;

    // Use this for initialization
    void Start () {
        SetupMoveBoundaries();
	}

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    // Update is called once per frame
    void Update () {
        Move();
        Fire();
    }

    private void Fire()
    {
        if(Input.GetButtonDown("Fire1")) {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if(Input.GetButtonUp("Fire1")) {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while(true) {
            GameObject lazer = Instantiate(LazerPrefab, transform.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, SFXVolume);
            lazer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(firingGap);
        }
        
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) {
            return;
        }
        DealDamage(collision, damageDealer);
    }

    private void DealDamage(Collider2D collision, DamageDealer damageDealer)
    {
        player_health -= damageDealer.getDamage();
        collision.gameObject.GetComponent<DamageDealer>().hit();
        if (player_health <= 0) {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer()
    {
        FindObjectOfType<Level>().GameOver(); 
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, SFXVolume);
        GameObject deathVFX = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(deathVFX, 1f);
    }

    public int getHealth()
    {
        return player_health;
    }
}
