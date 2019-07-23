using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField]float health = 100;
    [SerializeField] float ShotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float ProjectileSpeed = 10f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion=1f;
    [SerializeField] [Range(0,1)]float deathSoundVolume=0.7f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.27f;
    [SerializeField] AudioClip shootSound;
    int scoreValue = 100;


    // Start is called before the first frame update
    void Start()
    {
        ShotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownandShot();
    }
    private void CountDownandShot()
    {
        ShotCounter -= Time.deltaTime;
        if(ShotCounter<=0)
        {
            fire();
            ShotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
    private void fire()
    {
        GameObject Laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }
    private void OnTriggerEnter2D(Collider2D Other)
    {
        DamageDealer damageDealer = Other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, 1f);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
            

    }

}


