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

    private void Start()
    {
        SpawnAtLocation(Data.CheckpointNumber);
    }

    public void SpawnAtLocation(int pointNumber)
    {
        if (swarmPrefab != null)
        Instantiate(swarmPrefab, new Vector3(spawnPoints[pointNumber].position.x + 2, spawnPoints[pointNumber].position.y, spawnPoints[pointNumber].position.z), Quaternion.identity);
    }
}
