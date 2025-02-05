using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeOffOnStart : MonoBehaviour
{
    private Image image;

    void Start()
    {
        gameObject.SetActive(true);

        image = GetComponent<Image>();

        image.DOFade(0, 0.5f);
    }
}
