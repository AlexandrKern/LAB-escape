using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SoundEmitter : MonoBehaviour
{
    [SerializeField] private float _maxSoundRadius = 1f;
    private float _multiplier;
    private float _targetFillMethod;

    [Range(0, 1)]
    [SerializeField] private float _soundVolume = 0.5f;

    [SerializeField] private ProgressBar noiseLevelBar;

    private CircleCollider2D soundCollider;
    private MoveController moveController;

    private void Awake()
    {
        soundCollider = GetComponent<CircleCollider2D>();

        if (!soundCollider.isTrigger)
        {
            soundCollider.isTrigger = true;
        }

        moveController = GetComponentInParent<MoveController>();

        noiseLevelBar.SetFillMethod(UnityEngine.UI.Image.FillMethod.Horizontal);
    }

    private void Update()
    {
        if (moveController != null)
        {
            float currentSpeed = moveController.CurrentSpeed;
            _soundVolume = Mathf.Clamp01(currentSpeed / moveController.SoundSpeed);
            UpdateColliderSize();
            noiseLevelBar.SetFillAmount(NormalizeFillAmount());
        }
    }

    private void UpdateColliderSize()
    {
        switch (Character.Instance.GetCharacterForm())
        {
            case FormType.Base:
                _multiplier = 1f;
                _targetFillMethod = 2f;
                break;
            case FormType.Anthropomorphic:
                _multiplier = 2f;
                _targetFillMethod = 3.5f;
                break;
            case FormType.Hammer:
                _multiplier = 1f;
                _targetFillMethod = 2f;
                break;
            case FormType.Burglar:
                _multiplier = 4f;
                _targetFillMethod = 5f;
                break;
            case FormType.Mimicry:
                _multiplier = 1f;
                _targetFillMethod = 2f;
                break;
            case FormType.Mirror:
                _multiplier = 1.5f;
                _targetFillMethod = 2.7f;
                break;
            default:
                break;
        }
        float currentRadius = _soundVolume * _maxSoundRadius * _multiplier;
        soundCollider.radius = currentRadius;
    }
    
    private float NormalizeFillAmount()
    {
        return Mathf.Lerp(0,_targetFillMethod, moveController.CurrentSpeed/20);
    }

}













