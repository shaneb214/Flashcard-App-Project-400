using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.UI;

public class WallGameSettingsUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SliderManager repeatAmountSlider;
    [SerializeField] private CustomDropdown promptSelectionDropdown;
    [SerializeField] private Button btnBack;

    private void Awake()
    {
        btnBack.onClick.AddListener(OnBackButtonSelected);
    }

    private void Start()
    {
        repeatAmountSlider.minValue = WallGameSettingss.minRepeatCardCount;
        repeatAmountSlider.maxValue = WallGameSettingss.maxRepeatCardCount;


        //LanguageProfile currentLanguageProfile = LanguageProfileController.Instance.currentLanguageProfile;

        //Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentLanguageProfile.nativeLanguage.ISO}");
        //Sprite learningFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentLanguageProfile.learningLanguage.ISO}");

        //PASS SPRITES INTO HERE.
        promptSelectionDropdown.CreateNewItem("Learning", null);
        promptSelectionDropdown.CreateNewItem("Native", null);
    }

    private void OnBackButtonSelected()
    {
        //Update settings.
        WallGameSettingss.repeatCardAmount = (int)repeatAmountSlider.mainSlider.value;
        WallGameSettingss.promptSetting = (PromptSetting)promptSelectionDropdown.selectedItemIndex;
    }
}
