using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnLocations : MonoBehaviour
{
    /// <summary>
    /// Сюда помещаем все терминалы и точки спавна на карте. По этим трансформам будем спавнить персонажа.
    /// </summary>

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
             GameObject player =  Instantiate(swarmPrefab, spawnPoints[pointNumber].position, Quaternion.identity);
            _transformPlayer = player.transform;
        }
         
    }
}
