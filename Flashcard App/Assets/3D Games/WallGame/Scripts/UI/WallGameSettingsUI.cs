using Michsky.UI.ModernUIPack;
using UnityEngine;

public class WallGameSettingsUI : MonoBehaviour
{
    [SerializeField] private SliderManager repeatAmountSlider;
    [SerializeField] private CustomDropdown promptSelectionDropdown;

    private void Start()
    {
        repeatAmountSlider.minValue = 1;
        repeatAmountSlider.maxValue = WallGameDataSlinger.maxRepeatCardCount;


        //LanguageProfile currentLanguageProfile = LanguageProfileController.Instance.currentLanguageProfile;

        //Sprite nativeFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentLanguageProfile.nativeLanguage.ISO}");
        //Sprite learningFlagSprite = Resources.Load<Sprite>($"Prefabs/Sprites/Flags/{currentLanguageProfile.learningLanguage.ISO}");

        //PASS SPRITES INTO HERE.
        promptSelectionDropdown.CreateNewItem("Native", null);
        promptSelectionDropdown.CreateNewItem("Learning", null);
    }
}
