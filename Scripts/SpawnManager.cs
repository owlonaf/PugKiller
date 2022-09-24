using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    private float xRange = 8.0f;
    private float zBound = 30.0f;
    private bool isGameActive;
    private int boostRate = 5;
    private float enemyRate = 4.0f;

    public GameObject boost;
    public GameObject enemy;
    public GameObject titleScreen;
    public GameObject finalScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifesText;
    private PlayerController _playerController;

    private int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void StartSpawning(float difficulty)
    {
        enemyRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnBoosts());
        StartCoroutine(SpawnEnemy());
        titleScreen.gameObject.SetActive(false);
    }
    
    IEnumerator SpawnBoosts()
    {
        while (isGameActive == true)
        {
            yield return new WaitForSeconds(boostRate);
            float xCoordinate = Random.Range(-xRange, xRange);
            Vector3 spawnPos = new Vector3(xCoordinate, boost.transform.position.y, zBound);

            Instantiate(boost, spawnPos, boost.transform.rotation);
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (isGameActive == true)
        {
            yield return new WaitForSeconds(enemyRate);
            float xCoordinate = Random.Range(-xRange, xRange);
            Vector3 spawnPos = new Vector3(xCoordinate, boost.transform.position.y, zBound);

            Instantiate(enemy, spawnPos, enemy.transform.rotation);
        }
    }

    public void StopSpawning()
    {
        isGameActive = false;
        finalScreen.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLifes()
    {
        lifesText.text = "Lifes " + _playerController.lifesAmount;
    }
}