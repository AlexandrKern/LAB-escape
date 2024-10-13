using UnityEngine;
/// <summary>
///  ласс предотврощает вход в негабаритное место дл€ крупных форм персонажа
/// </summary>
public class BottleneckDetector : MonoBehaviour
{
    [SerializeField] Character _character;
    public static bool CanWeChangeTheForm { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*≈сли мы в форме молотка уперлись головой в потолкок или трубы, то возвращаемс€ в рой
         ћожно расширить дл€ других форм*/
        if(_character.GetCharacterForm() == FormType.Hammer)
        {
            if (collision.gameObject.layer == 3 || collision.gameObject.layer == 12)
            {
                Debug.Log("в молот нельз€");
                _character.ChangeForm(0);
                CanWeChangeTheForm = false;
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 12)
        {
            Debug.Log("в молот можно");
            CanWeChangeTheForm = true;
        }
    }

    private void OnDisable()
    {
        CanWeChangeTheForm = true;
    }
}
