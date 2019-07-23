using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour

{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 200;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.57f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.27f;
    [SerializeField] AudioClip shootSound;
    [Header("Projectile")]
    [SerializeField] GameObject LaserPrefab;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float ProjectileSpeed = 10f;
    [SerializeField] float ProjectileFiringPeriod = 0.1f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine FiringCoroutine;


    void Start()

    {
        SetupNewBoundries();
    }
    

    private void SetupNewBoundries()
    {
        Camera gamecamera = Camera.main;
        xMin = gamecamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x+padding;
        xMax = gamecamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-padding;
        yMin = gamecamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y+padding;
        yMax = gamecamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y-padding;

    }

    // Update is called once per frame
    void Update()

    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        DamageDealer damageDealer = Other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
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
        FindObjectOfType<SceneLoader>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);

    }
    public int GetHealth()
    {
        return health;
    }
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXpos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYpos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXpos, newYpos);




    }
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
            {
            FiringCoroutine=StartCoroutine(FireContinously()); 
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(FiringCoroutine);
        }
    }
    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject Laser = Instantiate(LaserPrefab, transform.position, Quaternion.identity) as GameObject;
            Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(ProjectileFiringPeriod);
        }
    }
    
}
