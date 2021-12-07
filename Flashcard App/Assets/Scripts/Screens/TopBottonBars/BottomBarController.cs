using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Bar at bottom of most screens where you can switch between main screens. 
//This controller ensures that the icons for these buttons are "dim" when their screens aren't active, ensure more than one of the same screen gets loaded etc. 

public class BottomBarController : MonoBehaviour
{
    [SerializeField] private BtnBottomBar btnNewCard;
    [SerializeField] private BtnBottomBar btnMySets;


    private void Start()
    {
        //btnNewCard.AddListenerToButtonClick()
    }

    private void OnBottomBarButtonPressed(BtnBottomBar buttonPressed)
    {

    }


    private void OnDestroy()
    {
       // BtnBottomBar.BottomBarClickedEvent -= OnBottomBarButtonPressed;
    }

}
