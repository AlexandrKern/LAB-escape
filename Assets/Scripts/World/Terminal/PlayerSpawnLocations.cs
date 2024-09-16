using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnLocations : MonoBehaviour
{
    /// <summary>
    /// ���� �������� ��� ��������� � ����� ������ �� �����. �� ���� ����������� ����� �������� ���������.
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
        Instantiate(swarmPrefab, spawnPoints[pointNumber].position, Quaternion.identity);


    }
}
