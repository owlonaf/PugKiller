using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    private float zUpBoundry = 30;
    private float zDownBoundary = -10;
    public ParticleSystem explosionFx;

    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        if (transform.position.z > zUpBoundry || transform.position.z < zDownBoundary)
        {
            Destroy(gameObject);
        }
    }
    
    public void Explode ()
    {
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Explode();
            _spawnManager.UpdateScore(7);
        }
    }
}
