using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDisplay : MonoBehaviour
{
    [Header("My Components")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private TextMeshProUGUI txtGameName;
    [SerializeField] private TextMeshProUGUI txtGameRequirement;
    [SerializeField] private TextMeshProUGUI txtGameAvailabilityStatus;
    [SerializeField] private Image imgGame;
    [SerializeField] private Image imgAvailabilityIcon;

    private const string gameAvailableText = "Available To Play";
    private const string gameUnavailableText = "Unavailable To Play";
    [SerializeField] private Sprite gameAvailableSprite;
    [SerializeField] private Sprite gameUnavailableSprite;
    [SerializeField] private Color gameAvailableColour;
    [SerializeField] private Color gameUnavailableColour;

    private bool canPlay;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayButtonPressed);
    }

    private void OnPlayButtonPressed()
    {
        if(canPlay)
        {
            SceneManager.LoadScene("WallDash");
        }
        //send over data?
    }

    public void UpdateDisplayInfoForGame(GameDisplayInfo gameDisplayInfo)
    {
        txtGameName.text = gameDisplayInfo.GameName;
        txtGameRequirement.text = gameDisplayInfo.MinimumRequirementsDescription;
        imgGame.sprite = gameDisplayInfo.gameSprite;
    }

    public void UpdateAvailabilityDisplay(bool gameAvailibleToPlay)
    {
        canPlay = gameAvailibleToPlay;

        if(canPlay)
        {
            txtGameAvailabilityStatus.text = gameAvailableText;
            imgAvailabilityIcon.sprite = gameAvailableSprite;
            imgAvailabilityIcon.color = gameAvailableColour;
            txtGameAvailabilityStatus.color = gameAvailableColour;
        }
        else
        {
            txtGameAvailabilityStatus.text = gameUnavailableText;
            imgAvailabilityIcon.sprite = gameUnavailableSprite;
            imgAvailabilityIcon.color = gameUnavailableColour;
            txtGameAvailabilityStatus.color = gameUnavailableColour;
        }
    }
}
