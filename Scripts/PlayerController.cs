using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float vMaxSpeed;
    public float hMaxSpeed;
    public float vAcceleration = 100;
    public float hAcceleration = 150;
    public GameObject playerAmmo;

    // private Vector3 startPos = new Vector3(0, 0.5f, 1);
    private Rigidbody playerRb;
    private float zDownBound = -2.0f;
    private float zUpBound = 4.0f;
    private float outsidePlayerAddition = 1.0f;
    public int lifesAmount = 3;
    public ParticleSystem enemyParticle;
    public ParticleSystem boostParticle;
    public bool isGameActive;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        isGameActive = true;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ShootProjectile();
        ConstrainPlayerPosition();
        // if (isGameActive == false)
        // {
        //     GameObject.Find("SpawnManager").GetComponent<SpawnManager>().StopSpawning();
        // }
    }

    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // transform.Translate(Vector3.forward * hMaxSpeed * Time.deltaTime * verticalInput, Space.World);
        // transform.Translate(Vector3.right * hMaxSpeed * Time.deltaTime * horizontalInput, Space.World);
        if (MathF.Abs(playerRb.velocity.z)  < vMaxSpeed)
        {
            playerRb.AddForce(Vector3.forward * (vAcceleration * verticalInput));
        }
        
        if (MathF.Abs(playerRb.velocity.x) < hMaxSpeed)
        {
            playerRb.AddForce(Vector3.right * (hAcceleration * horizontalInput));
        }
    }

    void ConstrainPlayerPosition()
    {
        if (transform.position.z < zDownBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zDownBound);
        }

        if (transform.position.z > zUpBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zUpBound);
        }
    }

    void ShootProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(playerAmmo,
                new Vector3(transform.position.x, transform.position.y, transform.position.z + outsidePlayerAddition),
                playerAmmo.transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("You were fucked");
            Explode(enemyParticle, collision.gameObject.transform.position);
            Destroy(collision.gameObject);
            _spawnManager.UpdateScore(-5);
            lifesAmount -= 1;
            _spawnManager.UpdateLifes();
            if (lifesAmount == 0)
            {
                GameObject.Find("SpawnManager").GetComponent<SpawnManager>().StopSpawning();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boost"))
        {
            Destroy(other.gameObject);
            Explode(boostParticle, other.gameObject.transform.position);
            _spawnManager.UpdateScore(10);
        }
    }

    public void Explode(ParticleSystem particleSystem, Vector3 position)
    {
        Instantiate(particleSystem, position, particleSystem.transform.rotation);
    }
}