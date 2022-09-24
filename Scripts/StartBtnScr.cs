using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBtnScr : MonoBehaviour
{
    private Button _startBtn;

    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _startBtn = GetComponent<Button>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _startBtn.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetDifficulty()
    {
        _spawnManager.StartSpawning();
        Debug.Log(_startBtn.gameObject.name);
    }
}