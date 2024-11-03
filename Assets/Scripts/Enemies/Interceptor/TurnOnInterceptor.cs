using UnityEngine;

public class TurnOnInterceptor : MonoBehaviour
{
    [SerializeField] private Interceptor interceptor;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interceptor.isActiv = true;
            interceptor.CheckIsActiv();
        }
        
    }
}
