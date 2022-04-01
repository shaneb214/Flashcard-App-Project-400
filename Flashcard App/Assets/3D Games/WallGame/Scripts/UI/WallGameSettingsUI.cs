using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.UI;

public class WallGameSettingsUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SliderManager repeatAmountSlider;
    [SerializeField] private CustomDropdown promptSelectionDropdown;
    [SerializeField] private Button btnBack;

    [Header("Settings to change")]
    [SerializeField] WallGameSettings wallGameSettings;

    private void Awake()
    {
        btnBack.onClick.AddListener(OnBackButtonSelected);
    }

    private void Start()
    {
        repeatAmountSlider.minValue = WallGameSettings.minRepeatCardCount;
        repeatAmountSlider.maxValue = WallGameSettings.maxRepeatCardCount;

        repeatAmountSlider.mainSlider.value = wallGameSettings.repeatCardAmount;


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
        wallGameSettings.repeatCardAmount = (int)repeatAmountSlider.mainSlider.value;
        wallGameSettings.promptSetting = (PromptSetting)promptSelectionDropdown.selectedItemIndex;
    }
}
