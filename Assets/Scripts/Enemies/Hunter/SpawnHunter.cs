using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHunter : MonoBehaviour
{
    [SerializeField] GameObject _hunterPrefub;

    public void Spawn(Transform spawnPoint)
    {
        Instantiate(_hunterPrefub,spawnPoint.position,Quaternion.identity);
    }
}
