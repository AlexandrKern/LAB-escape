using UnityEngine;
/// <summary>
/// Класс предотврощает вход в негабаритное место для крупных форм персонажа
/// </summary>
public class BottleneckDetector : MonoBehaviour
{
    [SerializeField] Character _character;
    public static bool CanWeChangeTheForm { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Если мы в форме молотка уперлись головой в потолкок или трубы, то возвращаемся в рой
         Можно расширить для других форм*/
        if(_character.GetCharacterForm() == FormType.Hammer)
        {
            if (collision.gameObject.layer == 3 || collision.gameObject.layer == 12)
            {
                _character.ChangeForm(0);
                CanWeChangeTheForm = false;
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 12)
        {
            CanWeChangeTheForm = true;
        }
    }

    private void OnDisable()
    {
        CanWeChangeTheForm = true;
    }
}
