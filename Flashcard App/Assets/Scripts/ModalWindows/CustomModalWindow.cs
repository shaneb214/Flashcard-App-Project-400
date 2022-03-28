using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class CustomModalWindow : MonoBehaviour
{
    [Header("Components")]
    protected Animator mwAnimator;
    [Header("Status")]
    [SerializeField] private bool isOn = false;
    [Header("Animations")]
    [SerializeField] private bool sharpAnimations = false;
    [Header("Start / Close Behaviour")]
    [SerializeField] private StartBehaviour startBehaviour = StartBehaviour.None;
    [SerializeField] private CloseBehaviour closeBehaviour = CloseBehaviour.Disable;

    public enum StartBehaviour { None, Disable }
    public enum CloseBehaviour { None, Disable, Destroy }

    public virtual void Awake()
    {
        isOn = false;

        if (mwAnimator == null) { mwAnimator = gameObject.GetComponent<Animator>(); }
        if (startBehaviour == StartBehaviour.Disable) { gameObject.SetActive(false); }
    }
    public virtual void Start() { }

    public void OpenWindow()
    {
        if (isOn == false)
        {
            StopCoroutine("DisableObject");
            gameObject.SetActive(true);
            isOn = true;

            if (sharpAnimations == false) { mwAnimator.CrossFade("Fade-in", 0.1f); }
            else { mwAnimator.Play("Fade-in"); }
        }
    }

    public void CloseWindow()
    {
        if (isOn == true)
        {
            StartCoroutine("DisableObject");
            isOn = false;

            if (sharpAnimations == false) { mwAnimator.CrossFade("Fade-out", 0.1f); }
            else { mwAnimator.Play("Fade-out"); }
        }
    }

    public void AnimateWindow()
    {
        if (isOn == false)
        {
            StopCoroutine("DisableObject");
            gameObject.SetActive(true);
            isOn = true;

            if (sharpAnimations == false) { mwAnimator.CrossFade("Fade-in", 0.1f); }
            else { mwAnimator.Play("Fade-in"); }
        }

        else
        {
            StartCoroutine("DisableObject");
            isOn = false;

            if (sharpAnimations == false) { mwAnimator.CrossFade("Fade-out", 0.1f); }
            else { mwAnimator.Play("Fade-out"); }
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(0.3f);

        if (closeBehaviour == CloseBehaviour.Disable) { gameObject.SetActive(false); }
        else if (closeBehaviour == CloseBehaviour.Destroy) { Destroy(gameObject); }
    }
}

