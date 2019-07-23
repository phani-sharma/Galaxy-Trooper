using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] GameObject PathPrefab;
    [SerializeField] float TimeBetweenSpawn=0.5f;
    [SerializeField] float SpawnRandomFactor=0.3f;
    [SerializeField] int NumberOfEnemies = 7;
    [SerializeField] float MoveSpeed=2f;


    public GameObject GetEnemyPrefab() { return EnemyPrefab; }
    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach(Transform child in PathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }

    public float GetTimeBetweenSpawn() { return TimeBetweenSpawn; }
    public float GetSpawnRandomFactor() { return SpawnRandomFactor;  }
    public int GetNumberOfEnemies() { return NumberOfEnemies; }
    public float GetMoveSpeed() { return MoveSpeed; }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
