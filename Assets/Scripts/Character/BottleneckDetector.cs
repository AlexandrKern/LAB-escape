using UnityEngine;
/// <summary>
/// ����� ������������� ���� � ������������ ����� ��� ������� ���� ���������
/// </summary>
public class BottleneckDetector : MonoBehaviour
{
    [SerializeField] Character _character;
    public static bool CanWeChangeTheForm { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*���� �� � ����� ������� �������� ������� � �������� ��� �����, �� ������������ � ���
         ����� ��������� ��� ������ ����*/
        if(_character.GetCharacterForm() == FormType.Hammer)
        {
            if (collision.gameObject.layer == 3 || collision.gameObject.layer == 12)
            {
                Debug.Log("� ����� ������");
                _character.ChangeForm(0);
                CanWeChangeTheForm = false;
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 12)
        {
            Debug.Log("� ����� �����");
            CanWeChangeTheForm = true;
        }
    }

    private void OnDisable()
    {
        CanWeChangeTheForm = true;
    }
}
