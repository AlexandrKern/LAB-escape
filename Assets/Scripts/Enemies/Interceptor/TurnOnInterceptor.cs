using UnityEngine;

public class TurnOnInterceptor : MonoBehaviour
{
    [SerializeField] private Interceptor interceptor;

    private void Start()
    {
        if (Data.IsHammerFormAvailable)
        {
            interceptor.isActiv = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interceptor.isActiv = true;
        }
        
    }
}
