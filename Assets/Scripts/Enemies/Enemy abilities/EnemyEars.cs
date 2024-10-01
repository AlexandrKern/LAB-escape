using UnityEngine;

public class EnemyEars : MonoBehaviour
{
    private bool _hear;
    public bool Hear => _hear;

    private Vector3 _lastPlayerPosition;
    public Vector3 LastPlayerPosition => _lastPlayerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       React(collision,true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        React(collision,true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        React(collision,false);
    }

    private void React(Collider2D collision,bool hear)
    {
        if (collision.CompareTag("SoundEmitter"))
        {
            if(collision != null)
            {
                _hear = hear;
                _lastPlayerPosition = collision.gameObject.GetComponentInParent<Transform>().position;
            }
        }
    }
}
