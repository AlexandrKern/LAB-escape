using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoolProof : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<HintController>().HintPressQForObstacle();
    }
}
