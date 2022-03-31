using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGameMainMenuController : MonoBehaviour
{
    private Camera myCamera;
    [SerializeField] private Transform cameraMoveTargetPos;


    private void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCameraMovementToRunningCharacter();
        }
    }

    private void StartCameraMovementToRunningCharacter()
    {
        LeanTween.move(myCamera.gameObject, cameraMoveTargetPos.position, 2f).setEaseInBack();
        LeanTween.rotate(myCamera.gameObject, cameraMoveTargetPos.rotation.eulerAngles, 2f);
    }
}
