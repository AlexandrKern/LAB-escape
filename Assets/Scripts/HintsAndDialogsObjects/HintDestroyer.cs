using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintDestroyer : MonoBehaviour
{
    [SerializeField] GameObject hintForDestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(hintForDestroy);
        }
    }
}
