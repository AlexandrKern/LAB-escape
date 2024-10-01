using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnLocations : MonoBehaviour
{
    [SerializeField] GameObject swarmPrefab;
    [SerializeField] Transform[] spawnPoints;

    [HideInInspector] public Transform _transformPlayer;
    private void Awake()
    {
        SpawnAtLocation(Data.CheckpointNumber);
    }

    //private void Start()
    //{
    //    SpawnAtLocation(Data.CheckpointNumber);
    //}

    public void SpawnAtLocation(int pointNumber)
    {
        if (swarmPrefab != null)
        {
            // немного сдвигаем спавн вправо, чтобы не спавниться на терминале
            Vector3 spawnPos = new Vector3(spawnPoints[pointNumber].position.x, spawnPoints[pointNumber].position.y, spawnPoints[pointNumber].position.z);
            GameObject player =  Instantiate(swarmPrefab, new Vector3(spawnPoints[pointNumber].position.x + 2, spawnPoints[pointNumber].position.y, spawnPoints[pointNumber].position.z), Quaternion.identity);
            _transformPlayer = player.transform;
        }
         
    }
}
