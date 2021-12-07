using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Bar at bottom of most screens where you can switch between main screens. 
//This controller ensures that the icons for these buttons are "dim" when their screens aren't active, ensure more than one of the same screen gets loaded etc. 

public class BottomBarController : MonoBehaviour
{
    [SerializeField] private BtnBottomBar btnNewCard;
    [SerializeField] private BtnBottomBar btnMySets;
    [SerializeField] private BtnBottomBar currentActiveButton;
    [SerializeField] private BtnBottomBar lastActiveButton;


    private void Start()
    {
        btnNewCard.BottomBarClickedEvent += OnBottomBarButtonPressed;
        btnMySets.BottomBarClickedEvent += OnBottomBarButtonPressed;

        currentActiveButton = btnNewCard;

        //Hard code reduce alpha of other buttons?
        btnMySets.SetLowAlpha();
    }

    private void OnBottomBarButtonPressed(BtnBottomBar buttonPressed)
    {
        lastActiveButton = currentActiveButton;
        lastActiveButton.SetLowAlpha();

        currentActiveButton = buttonPressed;
        currentActiveButton.SetMaxAlpha();
    }


    private void OnDestroy()
    {
        btnNewCard.BottomBarClickedEvent -= OnBottomBarButtonPressed;
        btnMySets.BottomBarClickedEvent -= OnBottomBarButtonPressed;
    }
}
