using UnityEngine;
using Cysharp.Threading.Tasks;

public class RandomSpriteFlasher : MonoBehaviour
{
    [SerializeField] private LineRenderer[] spriteRenderers;
    [SerializeField] private Animator animatorThunder;

    private async void Start()
    {
        while (true)
        {
            float randomDelaySeconds = Random.Range(0f, 5f);
            await UniTask.Delay((int)(randomDelaySeconds * 1000));
            //int randomIndex = Random.Range(0, spriteRenderers.Length);
            //spriteRenderers[randomIndex].enabled = true;
            //await UniTask.Delay(500);
            //spriteRenderers[randomIndex].enabled = false;
            animatorThunder?.SetTrigger("ThunderEffect");
        }
    }
}
