using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopEndPath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            Interceptor interceptor = collision.gameObject.GetComponent<Interceptor>();
            if (interceptor != null)
            {
                interceptor.ChangeState(new InterceptorPatrolState(interceptor));
            }
        }
    }
}
