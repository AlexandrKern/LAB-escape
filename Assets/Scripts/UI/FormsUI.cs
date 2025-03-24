using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormsUI : MonoBehaviour
{
    [Header("Main Forms UI")]
    [SerializeField] private GameObject gameObjectFormsUI;

    [Header("Base Form")]
    [SerializeField] private Image baseBorderColor;
    [SerializeField] private Image baseIconColor;

    [Header("Hammer Form")]
    [SerializeField] private Image hammerFormBorderColor;
    [SerializeField] private Image hammerFormIconColor;

    [Header("Burglar Form")]
    [SerializeField] private Image burglarFormBorderColor;
    [SerializeField] private Image burglarFormIconColor;

    [Header("Mirror Form")]
    [SerializeField] private Image mirrorBorderColor;
    [SerializeField] private Image mirrorIconColor;

    [Header("Mimicry Form")]
    [SerializeField] private Image mimicryBorderColor;
    [SerializeField] private Image mimicryIconColor;

    [Header("Anthropomorphic Form")]
    [SerializeField] private Image anthropomorphicBorderColor;
    [SerializeField] private Image anthropomorphicIconColor;

    [Header("Form Counters")]
    [SerializeField] private TextMeshProUGUI baseNumberText;
    [SerializeField] private TextMeshProUGUI hammerNumberText;
    [SerializeField] private TextMeshProUGUI burglarNumberText;
    [SerializeField] private TextMeshProUGUI mimicNumberText;
    [SerializeField] private TextMeshProUGUI mirrorNumberText;
    [SerializeField] private TextMeshProUGUI anthropomorphicNumberText;

    private readonly Color colorOn = new Color32(255, 255, 255, 255);
    private readonly Color colorOff = new Color32(73, 73, 73, 255);
    [SerializeField] Character character;

    private void Start()
    {
        CheckHammerAvailable();
        CheckColor();
    }

    public void CheckHammerAvailable()
    {
         gameObjectFormsUI.SetActive(Data.IsHammerFormAvailable);
    }

    public void CheckBurlgarAvailable()
    {
        burglarFormIconColor.gameObject.SetActive(true);
    }

    public void CheckColor()
    {
        var currentForm = character.GetCharacterForm();

        SetFormColors(FormType.Base, baseBorderColor, baseIconColor, baseNumberText, currentForm == FormType.Base);
        SetFormColors(FormType.Hammer, hammerFormBorderColor, hammerFormIconColor, hammerNumberText, currentForm == FormType.Hammer);
        SetFormColors(FormType.Burglar, burglarFormBorderColor, burglarFormIconColor, burglarNumberText, currentForm == FormType.Burglar);
        SetFormColors(FormType.Mirror, mirrorBorderColor, mirrorIconColor, mirrorNumberText, currentForm == FormType.Mirror);
        SetFormColors(FormType.Mimicry, mimicryBorderColor, mimicryIconColor, mimicNumberText, currentForm == FormType.Mimicry);
        SetFormColors(FormType.Anthropomorphic, anthropomorphicBorderColor, anthropomorphicIconColor, anthropomorphicNumberText, currentForm == FormType.Anthropomorphic);
    }

    private void SetFormColors(FormType formType, Image border, Image icon, TextMeshProUGUI numberText, bool isActive)
    {
        Color targetColor = isActive ? colorOn : colorOff;
        if (border != null) border.color = targetColor;
        if (icon != null) icon.color = targetColor;
        if (numberText != null) numberText.color = targetColor;
    }
}
